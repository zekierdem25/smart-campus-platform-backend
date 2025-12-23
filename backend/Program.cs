using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartCampus.API.Data;
using SmartCampus.API.Middleware;
using SmartCampus.API.Services;
using SmartCampus.API.Extensions.BackgroundServices;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.InMemory;

using PdfSharpCore.Fonts;

namespace SmartCampus.API;

public partial class Program
{
    public static void Main(string[] args)
    {
        // Configure Font Resolver for Linux (Cloud Run) support
        // Only set if not already set (prevents error in integration tests)
        if (GlobalFontSettings.FontResolver == null)
        {
            GlobalFontSettings.FontResolver = new CustomFontResolver();
        }

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Frontend'den gelen küçük harfli property'leri kabul et
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Database Configuration (skip for Testing environment - handled by test project)
        if (builder.Environment.EnvironmentName != "Testing")
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));
        }

        // JWT Authentication Configuration
        var jwtSecretKey = builder.Configuration["JWT:SecretKey"]
            ?? throw new InvalidOperationException("JWT:SecretKey is not configured");
        var key = Encoding.UTF8.GetBytes(jwtSecretKey);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // SignalR JWT Authentication - Get token from query string
            options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    
                    // If the request is for SignalR hub, get token from query string
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

        // Authorization
        builder.Services.AddAuthorization();

        // Memory Cache (Part 4: Security & Optimization)
        builder.Services.AddMemoryCache();

        // Register Services - Part 1
        builder.Services.AddScoped<IJwtService, JwtService>();
     builder.Services.AddScoped<IFileStorageService, GoogleCloudStorageService>();

// Add Email Service
builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IActivityLogService, ActivityLogService>();

        // Register Services - Part 2: Academic Management
        builder.Services.AddScoped<IPrerequisiteService, PrerequisiteService>();
        builder.Services.AddScoped<IScheduleConflictService, ScheduleConflictService>();
        builder.Services.AddScoped<IGradeCalculationService, GradeCalculationService>();
        builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

        // Register Services - Part 2: Attendance System
        builder.Services.AddScoped<IAttendanceService, AttendanceService>();
        builder.Services.AddScoped<ISpoofingDetectionService, SpoofingDetectionService>();

        // Register Services - Notification System
        builder.Services.AddScoped<INotificationService, NotificationService>();

        // Register Services - Payment System
        builder.Services.AddScoped<IPaymentService, PaymentService>();

        // Register Services - Scheduling System
        builder.Services.AddScoped<ISchedulingService, SchedulingService>();

        // Register Services - Analytics System (Part 4)
        builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
        builder.Services.AddScoped<IExportService, ExportService>();
        
        // Memory Cache must be registered before AnalyticsService
        // (Already added above)

        // Register Services - QR Code System
        builder.Services.AddSingleton<IQRCodeService, QRCodeService>();

        // Register Services - Background Jobs
        builder.Services.AddScoped<IEventReminderService, EventReminderService>();
        builder.Services.AddScoped<IWaitlistProcessingService, WaitlistProcessingService>();

        // Hangfire Configuration (In-Memory for development/testing, MySQL for production)
        builder.Services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
            
            // Use InMemory for development/testing, configure MySQL/SQL Server for production
            if (builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Testing")
            {
                config.UseInMemoryStorage();
            }
            else
            {
                // Production: Use InMemory for now (change to UseMySqlStorage when Hangfire.MySql is added)
                // Install Hangfire.MySql package and configure:
                // config.UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions()));
                config.UseInMemoryStorage();
            }
        });
        builder.Services.AddHangfireServer();

        // Register Background Services
        builder.Services.AddHostedService<AttendanceWarningJob>();

        // SignalR Configuration (Part 4)
        builder.Services.AddSignalR();

        // CORS Configuration
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                // Birden fazla origin destekle (örn: prod domain + localhost)
                var urlsFromArray = builder.Configuration.GetSection("Frontend:Urls").Get<string[]>();
                var singleUrl = builder.Configuration["Frontend:Url"];

                var origins = (urlsFromArray ?? Array.Empty<string>())
                    .Concat(string.IsNullOrWhiteSpace(singleUrl) ? Array.Empty<string>() : new[] { singleUrl })
                    .Select(o => o.Trim())
                    .Where(o => !string.IsNullOrWhiteSpace(o))
                    .Distinct()
                    .ToArray();

                if (builder.Environment.IsDevelopment())
                {
                    // Dev'de CORS kaynaklı takılmamak için tüm origin'lere izin ver
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                }
                else
                {
                    var allowedOrigins = origins.Length > 0 ? origins : new[] { "http://localhost:3000" };
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                }
            });
        });

        // -------------------------------
        // 🔥 Swagger Configuration + JWT UI
        // -------------------------------
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SmartCampus.API",
                Version = "v1"
            });

            // JWT Authentication için Swagger Authorization
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT tokeninizi 'Bearer <token>' şeklinde giriniz."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var app = builder.Build();

        // Database Migration - Production'da otomatik çalıştır
        if (app.Environment.EnvironmentName != "Testing")
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Migration hatası loglanır ama uygulama çalışmaya devam eder
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Database migration hatası");
                }
            }
        }

        // Hangfire Dashboard (Admin only in production)
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Smart Campus Background Jobs",
            // In development, allow anonymous access; in production, restrict to admin
            IsReadOnlyFunc = (context) => !app.Environment.IsDevelopment()
        });

        // Register recurring background jobs
        if (app.Environment.EnvironmentName != "Testing")
        {
            Extensions.BackgroundServices.BackgroundJobsRegistration.RegisterRecurringJobs();
        }

        // Configure the HTTP request pipeline
        app.UseErrorHandling();
        
        // Rate Limiting Middleware (Part 4)
        app.UseMiddleware<Middleware.RateLimitingMiddleware>();

        // Swagger - Development ve Production'da aktif
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Campus API v1");
            c.RoutePrefix = "swagger";
        });

        // HTTPS redirect production'da anlamlı; Development'da boş https port uyarısı ve CORS sorununa yol açmasın
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        // Static files for uploads
        var uploadsPath = Path.Combine(app.Environment.ContentRootPath, "uploads");
        Directory.CreateDirectory(uploadsPath);
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploadsPath),
            RequestPath = "/uploads"
        });

        app.UseCors("AllowFrontend");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // SignalR Hub Mapping (Part 4)
        app.MapHub<Hubs.NotificationHub>("/hubs/notifications");
        app.MapHub<Hubs.AttendanceHub>("/hubs/attendance");

        app.Run();
    }
}
