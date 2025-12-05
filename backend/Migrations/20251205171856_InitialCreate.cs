using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Faculty = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfilePictureUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEmailVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmailVerificationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerificationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailVerificationTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OfficeLocation = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OfficeHours = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Specialization = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculties_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faculties_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedByIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RevokedReason = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedByIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RevokedByIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GPA = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    CGPA = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    CurrentSemester = table.Column<int>(type: "int", nullable: false),
                    EnrollmentYear = table.Column<int>(type: "int", nullable: false),
                    IsScholarship = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "Faculty", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "BM", new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4099), "Bilgisayar Mühendisliği Bölümü", "Mühendislik Fakültesi", true, "Bilgisayar Mühendisliği", new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4210) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "EEM", new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4292), "Elektrik-Elektronik Mühendisliği Bölümü", "Mühendislik Fakültesi", true, "Elektrik-Elektronik Mühendisliği", new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4293) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "MM", new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4295), "Makine Mühendisliği Bölümü", "Mühendislik Fakültesi", true, "Makine Mühendisliği", new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4296) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "IsEmailVerified", "LastLoginAt", "LastName", "PasswordHash", "Phone", "ProfilePictureUrl", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 12, 5, 17, 18, 55, 44, DateTimeKind.Utc).AddTicks(211), "admin@smartcampus.com", "Admin", true, true, null, "User", "$2a$11$YGQNYf297HWYE1cBo/IqAOpo67w2iVmpcQw03hZxT0VNYWUu.3Rum", null, null, "Admin", new DateTime(2025, 12, 5, 17, 18, 55, 44, DateTimeKind.Utc).AddTicks(438) },
                    { new Guid("c1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 5, 17, 18, 55, 408, DateTimeKind.Utc).AddTicks(8210), "zeki.erdem@smartcampus.com", "Zeki", true, true, null, "Erdem", "$2a$11$VR/CI6myzwqMI4rDbxowQeU9fsas/eqfZNVIHNCMPUaZSveyz8z/q", null, null, "Student", new DateTime(2025, 12, 5, 17, 18, 55, 408, DateTimeKind.Utc).AddTicks(8215) },
                    { new Guid("c2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 5, 17, 18, 55, 529, DateTimeKind.Utc).AddTicks(8861), "mert.abdullahoglu@smartcampus.com", "Mert", true, true, null, "Abdullahoğlu", "$2a$11$YHKwZgwtTrFRcg0cB/G4nuxRhL5QKynG3A2FJ6eI/sJtyRqp.J6/y", null, null, "Student", new DateTime(2025, 12, 5, 17, 18, 55, 529, DateTimeKind.Utc).AddTicks(8866) },
                    { new Guid("c3333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 5, 17, 18, 55, 650, DateTimeKind.Utc).AddTicks(3999), "sena.kamiloglu@smartcampus.com", "Sena", true, true, null, "Kamiloğlu", "$2a$11$Dq9HVo27eOuWbb.gvG9Bl.siAdwc2sL65lJURQJJr.ipvqKvLCvwS", null, null, "Student", new DateTime(2025, 12, 5, 17, 18, 55, 650, DateTimeKind.Utc).AddTicks(4003) },
                    { new Guid("c4444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 5, 17, 18, 55, 770, DateTimeKind.Utc).AddTicks(1892), "sevval.asi@smartcampus.com", "Şevval", true, true, null, "Asi", "$2a$11$cN.ELCYptOp4tQMk71nAte4u//RVR1r3IPHGAPGPXfGawdzP2rsTu", null, null, "Student", new DateTime(2025, 12, 5, 17, 18, 55, 770, DateTimeKind.Utc).AddTicks(1972) },
                    { new Guid("c5555555-5555-5555-5555-555555555555"), new DateTime(2025, 12, 5, 17, 18, 55, 890, DateTimeKind.Utc).AddTicks(3418), "ali.veli@smartcampus.com", "Ali", true, true, null, "Veli", "$2a$11$2/XUiiWCzTp0uiHPYINoSOagrBOmfFSfu6YxoQgRHrLSOJMeD4TSe", null, null, "Student", new DateTime(2025, 12, 5, 17, 18, 55, 890, DateTimeKind.Utc).AddTicks(3422) },
                    { new Guid("f1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 5, 17, 18, 55, 168, DateTimeKind.Utc).AddTicks(5121), "mehmet.sevri@smartcampus.com", "Mehmet", true, true, null, "Sevri", "$2a$11$n1Gg6g3QBeIG5WJ3Bgj/NeJWUnr04CZXmMhM6rlDFzK3kDRDUJ0DK", null, null, "Faculty", new DateTime(2025, 12, 5, 17, 18, 55, 168, DateTimeKind.Utc).AddTicks(5126) },
                    { new Guid("f2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(5872), "ayse.yilmaz@smartcampus.com", "Ayşe", true, true, null, "Yılmaz", "$2a$11$3Kpj.mvVjuyk1G1jYVIvDeA2BFqCkSJCcxPlzXxViDFCZXgSowEre", null, null, "Faculty", new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(5876) }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "CreatedAt", "DepartmentId", "EmployeeNumber", "OfficeHours", "OfficeLocation", "Specialization", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("fa111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9078), new Guid("11111111-1111-1111-1111-111111111111"), "F001", "Pazartesi 10:00-12:00, Çarşamba 14:00-16:00", "A-101", "Web Programlama, Yazılım Mühendisliği", "AssociateProfessor", new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9169), new Guid("f1111111-1111-1111-1111-111111111111") },
                    { new Guid("fa222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9272), new Guid("22222222-2222-2222-2222-222222222222"), "F002", "Salı 09:00-11:00, Perşembe 13:00-15:00", "B-205", "Elektronik, Sinyal İşleme", "Professor", new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9273), new Guid("f2222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CGPA", "CreatedAt", "CurrentSemester", "DepartmentId", "EnrollmentYear", "GPA", "IsScholarship", "StudentNumber", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("d1111111-1111-1111-1111-111111111111"), 3.45m, new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6074), 7, new Guid("11111111-1111-1111-1111-111111111111"), 2021, 3.50m, true, "2021001", new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6188), new Guid("c1111111-1111-1111-1111-111111111111") },
                    { new Guid("d2222222-2222-2222-2222-222222222222"), 3.15m, new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6314), 7, new Guid("11111111-1111-1111-1111-111111111111"), 2021, 3.20m, false, "2021002", new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6314), new Guid("c2222222-2222-2222-2222-222222222222") },
                    { new Guid("d3333333-3333-3333-3333-333333333333"), 3.75m, new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6319), 7, new Guid("11111111-1111-1111-1111-111111111111"), 2021, 3.80m, true, "2021003", new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6319), new Guid("c3333333-3333-3333-3333-333333333333") },
                    { new Guid("d4444444-4444-4444-4444-444444444444"), 3.55m, new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6323), 7, new Guid("11111111-1111-1111-1111-111111111111"), 2021, 3.60m, false, "2021004", new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6323), new Guid("c4444444-4444-4444-4444-444444444444") },
                    { new Guid("d5555555-5555-5555-5555-555555555555"), 2.85m, new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6327), 5, new Guid("22222222-2222-2222-2222-222222222222"), 2022, 2.90m, false, "2022001", new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6327), new Guid("c5555555-5555-5555-5555-555555555555") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Code",
                table: "Departments",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_Token",
                table: "EmailVerificationTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_UserId",
                table: "EmailVerificationTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_DepartmentId",
                table: "Faculties",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_EmployeeNumber",
                table: "Faculties",
                column: "EmployeeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_UserId",
                table: "Faculties",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_Token",
                table: "PasswordResetTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserId",
                table: "PasswordResetTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentNumber",
                table: "Students",
                column: "StudentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailVerificationTokens");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
