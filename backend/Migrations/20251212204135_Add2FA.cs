using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class Add2FA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TwoFactorCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TempToken = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsUsed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    LockoutEndAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TwoFactorCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6072), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6173) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6299), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6299) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6304), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6304) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6307), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6308) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6311), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(6312) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1675), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1844), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1844) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1854), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1855) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1880), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1884), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1884) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1887), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(1888) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8631), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8737) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8834), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8835) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8844), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8844) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8847), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8848) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8851), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8852) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8855), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8868) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8872), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8872) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8876), new DateTime(2025, 12, 12, 20, 41, 35, 103, DateTimeKind.Utc).AddTicks(8876) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 106, DateTimeKind.Utc).AddTicks(4661), new DateTime(2025, 12, 12, 20, 41, 34, 106, DateTimeKind.Utc).AddTicks(4751) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 106, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 12, 12, 20, 41, 34, 106, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 106, DateTimeKind.Utc).AddTicks(4833), new DateTime(2025, 12, 12, 20, 41, 34, 106, DateTimeKind.Utc).AddTicks(4833) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(2881), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(2712), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(2955) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3028), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3047), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3046), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3047) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3051) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3057), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3056), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3057) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3061) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3064), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3063), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3064) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3067), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3067), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3068) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3071), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3071) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3074), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3074), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3075) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3078), new DateTime(2025, 11, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3077), new DateTime(2025, 12, 12, 20, 41, 35, 104, DateTimeKind.Utc).AddTicks(3078) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 515, DateTimeKind.Utc).AddTicks(2157), new DateTime(2025, 12, 12, 20, 41, 34, 515, DateTimeKind.Utc).AddTicks(2245) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 515, DateTimeKind.Utc).AddTicks(2326), new DateTime(2025, 12, 12, 20, 41, 34, 515, DateTimeKind.Utc).AddTicks(2327) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1016), new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1109) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1251), new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1251) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1256), new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1257) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1260), new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1261) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1277), new DateTime(2025, 12, 12, 20, 41, 35, 102, DateTimeKind.Utc).AddTicks(1277) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 279, DateTimeKind.Utc).AddTicks(9008), "$2a$11$DHwVSZEmksTMiFYVkrggMePAfaR3LHs95UUfnxSGbHqUWsIVdVvAm", new DateTime(2025, 12, 12, 20, 41, 34, 279, DateTimeKind.Utc).AddTicks(9151) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 632, DateTimeKind.Utc).AddTicks(4377), "$2a$11$4hb7pZudUXGP1DdHY8b.N.PCgfDobgbC3Yg.HU6LEqt7UhjciJLLm", new DateTime(2025, 12, 12, 20, 41, 34, 632, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 752, DateTimeKind.Utc).AddTicks(4766), "$2a$11$PlHdvwk3PrvHY8CntbMnX.mqRX/wzHdsVZA/0nifWXBXVA.7EnEra", new DateTime(2025, 12, 12, 20, 41, 34, 752, DateTimeKind.Utc).AddTicks(4769) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 871, DateTimeKind.Utc).AddTicks(6885), "$2a$11$48gli4jsR9D4XydbXAqlgOVt4OhqzGPEuY2cQaZ.M5EiVafcsCp6y", new DateTime(2025, 12, 12, 20, 41, 34, 871, DateTimeKind.Utc).AddTicks(6982) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 986, DateTimeKind.Utc).AddTicks(2185), "$2a$11$GlmTaPIngiXihaP3JwOwZuGGvd/kmeQM7Tqnif2px6TEoZPk3Brre", new DateTime(2025, 12, 12, 20, 41, 34, 986, DateTimeKind.Utc).AddTicks(2188) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 35, 100, DateTimeKind.Utc).AddTicks(6592), "$2a$11$PK3pDDLGyCg9xTzcIebzOupv7PJEuIpcQRbrEvR0IIb5BdeaDN4o2", new DateTime(2025, 12, 12, 20, 41, 35, 100, DateTimeKind.Utc).AddTicks(6597) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 399, DateTimeKind.Utc).AddTicks(4721), "$2a$11$wSvWsl5qUj1Fz5snzsCMnenKnJT3kHNiOi.mt/lbbBsjIysz/7CNa", new DateTime(2025, 12, 12, 20, 41, 34, 399, DateTimeKind.Utc).AddTicks(4725) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 20, 41, 34, 514, DateTimeKind.Utc).AddTicks(8886), "$2a$11$.du1PcPMG8rBcoWHZSMBteVUUc9yxNKAxIbazHA.G3QONJ6mwpNre", new DateTime(2025, 12, 12, 20, 41, 34, 514, DateTimeKind.Utc).AddTicks(8891) });

            migrationBuilder.CreateIndex(
                name: "IX_TwoFactorCodes_UserId",
                table: "TwoFactorCodes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TwoFactorCodes");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(2842), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(2925) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3009), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3010) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3023), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3024) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3027), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3027) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3029), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7344), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7423) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7501), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7502) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7505), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7506) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7512), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7512) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7531) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7534), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7534) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(4859), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(4964) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5041), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5042) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5045), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5046) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5048), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5049) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5055), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5055) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5057), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5058) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5073), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5074) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5076), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5077) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 28, 999, DateTimeKind.Utc).AddTicks(9448), new DateTime(2025, 12, 12, 19, 57, 28, 999, DateTimeKind.Utc).AddTicks(9545) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 28, 999, DateTimeKind.Utc).AddTicks(9633), new DateTime(2025, 12, 12, 19, 57, 28, 999, DateTimeKind.Utc).AddTicks(9634) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 28, 999, DateTimeKind.Utc).AddTicks(9636), new DateTime(2025, 12, 12, 19, 57, 28, 999, DateTimeKind.Utc).AddTicks(9636) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8443), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8297), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8516) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8592), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8590), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8592) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8597), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8596), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8597) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8601) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8604), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8603), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8604) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8607), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8607), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8607) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8613), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8612), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8613) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8616), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8616), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8616) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8619), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8623), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8622), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8623) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8626), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8626), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8626) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 428, DateTimeKind.Utc).AddTicks(1964), new DateTime(2025, 12, 12, 19, 57, 29, 428, DateTimeKind.Utc).AddTicks(2055) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 428, DateTimeKind.Utc).AddTicks(2135), new DateTime(2025, 12, 12, 19, 57, 29, 428, DateTimeKind.Utc).AddTicks(2136) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(8867), new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(8952) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9068), new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9068) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9080), new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9081) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9084), new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9085) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9088), new DateTime(2025, 12, 12, 19, 57, 29, 999, DateTimeKind.Utc).AddTicks(9088) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 180, DateTimeKind.Utc).AddTicks(3340), "$2a$11$NyO0u2cCyaFxma2C2dCgk.YJ5DlU53Z.NTZ/b2hFCu9ReXCsQy/Rm", new DateTime(2025, 12, 12, 19, 57, 29, 180, DateTimeKind.Utc).AddTicks(3476) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 544, DateTimeKind.Utc).AddTicks(7797), "$2a$11$sCrasAMAWVuHCaC0rAnAsePZOi9Zss8V0YDKccsufNr7H8i3TQ2x2", new DateTime(2025, 12, 12, 19, 57, 29, 544, DateTimeKind.Utc).AddTicks(7802) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 658, DateTimeKind.Utc).AddTicks(9597), "$2a$11$SFeQCSElaUU2NekCGVBvC.sBJ3r1UXBURaPmSVBOuWE.rR5Vd45im", new DateTime(2025, 12, 12, 19, 57, 29, 658, DateTimeKind.Utc).AddTicks(9601) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 772, DateTimeKind.Utc).AddTicks(6744), "$2a$11$KVie6Hedgo4UIKgg4fVryuUpP7uGSO22jaWC0hZ5BgDD1pVC8JWQG", new DateTime(2025, 12, 12, 19, 57, 29, 772, DateTimeKind.Utc).AddTicks(6746) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 885, DateTimeKind.Utc).AddTicks(3403), "$2a$11$O9C28yOektTf71Sq1pDZ8eO/g51cIIL8upNc1YphqHcJqmvDW..Vy", new DateTime(2025, 12, 12, 19, 57, 29, 885, DateTimeKind.Utc).AddTicks(3466) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 998, DateTimeKind.Utc).AddTicks(5707), "$2a$11$DSXi0GZEahRi4tHM33k8aOqfa1fTJ/uyOgF3EUVcOz2uqv7oPZbh.", new DateTime(2025, 12, 12, 19, 57, 29, 998, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 313, DateTimeKind.Utc).AddTicks(3257), "$2a$11$dlvwVq724bP9smGgTQ3U9.ZmJUxKrfjLdWevwFoz5qlc7zqkCwgRq", new DateTime(2025, 12, 12, 19, 57, 29, 313, DateTimeKind.Utc).AddTicks(3262) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 57, 29, 427, DateTimeKind.Utc).AddTicks(8280), "$2a$11$d3LeoBx.GVjs19RxfoxJfuI9FFnVJhUkPSDP2Xd1HuQyezMVlNaJ6", new DateTime(2025, 12, 12, 19, 57, 29, 427, DateTimeKind.Utc).AddTicks(8284) });
        }
    }
}
