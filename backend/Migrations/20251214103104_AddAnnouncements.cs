using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnouncements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsImportant = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Announcements_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_AuthorId",
                table: "Announcements",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CourseId_CreatedAt",
                table: "Announcements",
                columns: new[] { "CourseId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4192), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4271) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4354), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4355) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4359), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4359) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4362), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4362) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4365), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(4365) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9158), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9233) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9307), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9307) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9319), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9319) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9341), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9341) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9344), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9345) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9347), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(9348) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6523), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6629) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6712), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6713) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6722), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6722) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6725), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6725) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6727), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6728) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6730), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6744), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6744) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6747), new DateTime(2025, 12, 14, 9, 14, 13, 13, DateTimeKind.Utc).AddTicks(6747) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 13, DateTimeKind.Utc).AddTicks(9594), new DateTime(2025, 12, 14, 9, 14, 12, 13, DateTimeKind.Utc).AddTicks(9681) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 13, DateTimeKind.Utc).AddTicks(9757), new DateTime(2025, 12, 14, 9, 14, 12, 13, DateTimeKind.Utc).AddTicks(9757) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 13, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 12, 14, 9, 14, 12, 13, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(235), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(91), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(303) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(377), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(375), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(377) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(381) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(384), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(383), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(384) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(389), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(389), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(393), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(392), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(393) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(396), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(395), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(396) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(399), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(399), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(399) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(402), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(402), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(403) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(406), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(405), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(406) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(409), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(408), new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(409) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 411, DateTimeKind.Utc).AddTicks(4414), new DateTime(2025, 12, 14, 9, 14, 12, 411, DateTimeKind.Utc).AddTicks(4507) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 411, DateTimeKind.Utc).AddTicks(4670), new DateTime(2025, 12, 14, 9, 14, 12, 411, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(158), new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(245) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(355), new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(356) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(360), new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(361) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(364), new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(364) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(378), new DateTime(2025, 12, 14, 9, 14, 13, 12, DateTimeKind.Utc).AddTicks(378) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 177, DateTimeKind.Utc).AddTicks(7155), "$2a$11$vbTdpPSj8Swh9Dhob9buAuiRzh0Yd4nwdEvjmEDtYu2rFqKGe1xc6", new DateTime(2025, 12, 14, 9, 14, 12, 177, DateTimeKind.Utc).AddTicks(7281) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 546, DateTimeKind.Utc).AddTicks(5055), "$2a$11$CzBt7mTY2jwqMq2/XPsjhe/4hPFHiCuOmN2j3NZWhLKBqdf7Yrmcu", new DateTime(2025, 12, 14, 9, 14, 12, 546, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 662, DateTimeKind.Utc).AddTicks(5987), "$2a$11$lbzr02bamRaPU1jV7.3sneHUJfTp3ru2l9BoPRYFd9v6gr2Uz7ucO", new DateTime(2025, 12, 14, 9, 14, 12, 662, DateTimeKind.Utc).AddTicks(5992) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 777, DateTimeKind.Utc).AddTicks(8194), "$2a$11$Iu4zhID5ZQTjeZQVZonEAOkTwIR/jV8z75njntNWemsIfC5TGF6kO", new DateTime(2025, 12, 14, 9, 14, 12, 777, DateTimeKind.Utc).AddTicks(8278) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 895, DateTimeKind.Utc).AddTicks(2957), "$2a$11$vnbrZ3IIgxuB9uzg40T3vu41sILiGWxhfs0xJa7s6OPhSMquvKKeO", new DateTime(2025, 12, 14, 9, 14, 12, 895, DateTimeKind.Utc).AddTicks(2961) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 10, DateTimeKind.Utc).AddTicks(7008), "$2a$11$Wz4hcafsycKeEIcpkB27EO0rRz2YcWPeHCly2Byoxvb71rnkgQg8.", new DateTime(2025, 12, 14, 9, 14, 13, 10, DateTimeKind.Utc).AddTicks(7011) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 294, DateTimeKind.Utc).AddTicks(9381), "$2a$11$lbmS4qyhOYbqE8GH8bNvbOcSq.JT5PcdHUyiOx.AfRzIIGY9puijm", new DateTime(2025, 12, 14, 9, 14, 12, 294, DateTimeKind.Utc).AddTicks(9385) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 12, 411, DateTimeKind.Utc).AddTicks(94), "$2a$11$RYQM5ziV3kClnRNIiEK2IegTfFrxWl53SenHdFeA/zS7WWV5mY4XK", new DateTime(2025, 12, 14, 9, 14, 12, 411, DateTimeKind.Utc).AddTicks(100) });
        }
    }
}
