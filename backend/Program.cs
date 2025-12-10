using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartCampus.API.Data;
using SmartCampus.API.Middleware;
using SmartCampus.API.Services;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

namespace SmartCampus.API;

public partial class Program
{
    public static void Main(string[] args)
    {
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
        });

        // Authorization
        builder.Services.AddAuthorization();

        // Register Services
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IActivityLogService, ActivityLogService>();

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

        // Configure the HTTP request pipeline
        app.UseErrorHandling();

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

        app.Run();
    }
}
