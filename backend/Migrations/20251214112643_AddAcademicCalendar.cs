using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicEvents", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7261), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7351) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7436), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7436) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7449), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7449) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7452), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(7453) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2003), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2079) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2152), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2152) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2158), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2158) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2161), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2162) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2165), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2165) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2168), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2168) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9523), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9605) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9700), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9703), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9704) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9706), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9707) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9710), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9712), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9713) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9720), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9721) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9723), new DateTime(2025, 12, 14, 11, 26, 42, 925, DateTimeKind.Utc).AddTicks(9723) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 41, 886, DateTimeKind.Utc).AddTicks(1749), new DateTime(2025, 12, 14, 11, 26, 41, 886, DateTimeKind.Utc).AddTicks(1843) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 41, 886, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 12, 14, 11, 26, 41, 886, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 41, 886, DateTimeKind.Utc).AddTicks(1942), new DateTime(2025, 12, 14, 11, 26, 41, 886, DateTimeKind.Utc).AddTicks(1943) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3101), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(2962), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3171) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3245), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3243), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3245) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3248), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3248), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3249) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3253), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3252), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3253) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3256), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3256), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3256) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3259), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3259), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3262), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3262), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3263) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3266), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3265), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3266) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3271), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3271) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3274), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3273), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3274) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3277), new DateTime(2025, 11, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3276), new DateTime(2025, 12, 14, 11, 26, 42, 926, DateTimeKind.Utc).AddTicks(3277) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 339, DateTimeKind.Utc).AddTicks(7506), new DateTime(2025, 12, 14, 11, 26, 42, 339, DateTimeKind.Utc).AddTicks(7599) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 339, DateTimeKind.Utc).AddTicks(7684), new DateTime(2025, 12, 14, 11, 26, 42, 339, DateTimeKind.Utc).AddTicks(7684) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3393), new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3481) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3591), new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3592) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3599), new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3599) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3603), new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3603) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3610), new DateTime(2025, 12, 14, 11, 26, 42, 924, DateTimeKind.Utc).AddTicks(3610) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 99, DateTimeKind.Utc).AddTicks(802), "$2a$11$WlhqkLIEvkH016iqDQR.pOWzOJ.urngAz/U2Dbnlp3eiIbny0tccC", new DateTime(2025, 12, 14, 11, 26, 42, 99, DateTimeKind.Utc).AddTicks(949) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 457, DateTimeKind.Utc).AddTicks(6853), "$2a$11$dGcUbcAIGICQVikkZQ8d/evS6DJd6TknqG2xLw4cQvWAPr7o/wab6", new DateTime(2025, 12, 14, 11, 26, 42, 457, DateTimeKind.Utc).AddTicks(6857) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 574, DateTimeKind.Utc).AddTicks(8358), "$2a$11$.R2zH9UoAulnhXLO3BSFMuDsxc8elMjnlayOawO5W3h3QKBNxKb32", new DateTime(2025, 12, 14, 11, 26, 42, 574, DateTimeKind.Utc).AddTicks(8362) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 690, DateTimeKind.Utc).AddTicks(9544), "$2a$11$AcrYZx1Z8aeRFQ.IgMtlXuSNCfAHFFrWEmB496GFVbVxPMpjZq51C", new DateTime(2025, 12, 14, 11, 26, 42, 690, DateTimeKind.Utc).AddTicks(9611) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 804, DateTimeKind.Utc).AddTicks(7273), "$2a$11$S.xGmqo7bc1ws57MI/o3FutY2pu52/HANqrucAODlLECzxW/3lQhu", new DateTime(2025, 12, 14, 11, 26, 42, 804, DateTimeKind.Utc).AddTicks(7276) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 923, DateTimeKind.Utc).AddTicks(742), "$2a$11$j3zHXLmU5jhBnW.1PXAtIeY/T4JQNv/JDjAobJ2kBmaZg4KbYn4sa", new DateTime(2025, 12, 14, 11, 26, 42, 923, DateTimeKind.Utc).AddTicks(745) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 220, DateTimeKind.Utc).AddTicks(2064), "$2a$11$Ch6huOl3ueLNRKgWyMWlU.WooDSXWQ7BNxojNfEVXlK/7LPIRGEAa", new DateTime(2025, 12, 14, 11, 26, 42, 220, DateTimeKind.Utc).AddTicks(2068) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 26, 42, 339, DateTimeKind.Utc).AddTicks(4519), "$2a$11$NZyQZzlb2kCtwIoxEo5LOuM5GWCg.Xg7J.x0lLVIj49P28IuY6HoC", new DateTime(2025, 12, 14, 11, 26, 42, 339, DateTimeKind.Utc).AddTicks(4523) });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicEvents_StartDate_EndDate",
                table: "AcademicEvents",
                columns: new[] { "StartDate", "EndDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicEvents");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7092), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7172) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7249), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7250) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7253), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7253) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7262), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7263) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7278), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(7278) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1456), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1530) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1603), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1603) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1608), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1609) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1612), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1612) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1615), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1616) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1619), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(1619) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9022), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9099) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9171), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9172) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9175), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9175) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9177), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9178) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9180), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9183), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9183) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9190), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9190) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9192), new DateTime(2025, 12, 14, 10, 31, 3, 530, DateTimeKind.Utc).AddTicks(9193) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 544, DateTimeKind.Utc).AddTicks(630), new DateTime(2025, 12, 14, 10, 31, 2, 544, DateTimeKind.Utc).AddTicks(723) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 544, DateTimeKind.Utc).AddTicks(807), new DateTime(2025, 12, 14, 10, 31, 2, 544, DateTimeKind.Utc).AddTicks(807) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 544, DateTimeKind.Utc).AddTicks(810), new DateTime(2025, 12, 14, 10, 31, 2, 544, DateTimeKind.Utc).AddTicks(811) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2535), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2602) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2676), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2673), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2676) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2679), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2679), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2683), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2682), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2683) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2686), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2685), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2686) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2689), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2689), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2689) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2692), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2692), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2693) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2696), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2695), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2696) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2701), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2701) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2704), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2704), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2704) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2707), new DateTime(2025, 11, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2707), new DateTime(2025, 12, 14, 10, 31, 3, 531, DateTimeKind.Utc).AddTicks(2708) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 945, DateTimeKind.Utc).AddTicks(6393), new DateTime(2025, 12, 14, 10, 31, 2, 945, DateTimeKind.Utc).AddTicks(6478) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 945, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 12, 14, 10, 31, 2, 945, DateTimeKind.Utc).AddTicks(6561) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3298), new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3385) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3496), new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3497) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3501), new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3501) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3505), new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3505) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3518), new DateTime(2025, 12, 14, 10, 31, 3, 529, DateTimeKind.Utc).AddTicks(3519) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 715, DateTimeKind.Utc).AddTicks(4967), "$2a$11$1K/LXMFAbfClakFchmGNI.EmC3p149DRq3WET5OlfgA/NxQxsCRJi", new DateTime(2025, 12, 14, 10, 31, 2, 715, DateTimeKind.Utc).AddTicks(5115) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 63, DateTimeKind.Utc).AddTicks(631), "$2a$11$rD0k8zSSLGoLjs4x3qGm7e3odcOCLTpeB74UqDLaYwS1/lHoR7Kgm", new DateTime(2025, 12, 14, 10, 31, 3, 63, DateTimeKind.Utc).AddTicks(639) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 185, DateTimeKind.Utc).AddTicks(2384), "$2a$11$tmPeCZ5R.H08YRoBrM9eEeFyrAfJWqo/oDZyVr.YjpYwPykAOLQ8q", new DateTime(2025, 12, 14, 10, 31, 3, 185, DateTimeKind.Utc).AddTicks(2388) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 299, DateTimeKind.Utc).AddTicks(975), "$2a$11$fwlR6zEpelQXxJIhpL0c5OnMhJI7u6RQNMTqLUTiy2HNzn7pCqwwC", new DateTime(2025, 12, 14, 10, 31, 3, 299, DateTimeKind.Utc).AddTicks(1053) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 413, DateTimeKind.Utc).AddTicks(5107), "$2a$11$4ml/WCNN.qH6NdyFjA5r0.YPCgXbfCMk.3oPrzp/YGHArKb6.pk.y", new DateTime(2025, 12, 14, 10, 31, 3, 413, DateTimeKind.Utc).AddTicks(5111) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 3, 528, DateTimeKind.Utc).AddTicks(336), "$2a$11$aolt1CgloI2/bcMQkM7oiO0TBVfSUWU5zu3.b22fU1Le5doGn0HQG", new DateTime(2025, 12, 14, 10, 31, 3, 528, DateTimeKind.Utc).AddTicks(341) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 830, DateTimeKind.Utc).AddTicks(3811), "$2a$11$hYGSCgBp58nRU07DJeMK6.0z14boEkcd8ilvfQvvLeQ11hYu9SF7i", new DateTime(2025, 12, 14, 10, 31, 2, 830, DateTimeKind.Utc).AddTicks(3817) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 31, 2, 945, DateTimeKind.Utc).AddTicks(2927), "$2a$11$zMoFVHHPDJSAy.UdwOZZFOTZqhMIZu5jzY6HQK/P3SEexlEjBK9/m", new DateTime(2025, 12, 14, 10, 31, 2, 945, DateTimeKind.Utc).AddTicks(2933) });
        }
    }
}
