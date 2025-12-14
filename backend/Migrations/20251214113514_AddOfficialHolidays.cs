using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOfficialHolidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AcademicEvents",
                columns: new[] { "Id", "CreatedAt", "Description", "EndDate", "StartDate", "Title", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ae000001-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2181), "Yeni Yıl Tatili", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2261) },
                    { new Guid("ae000002-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2348), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2025, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2349) },
                    { new Guid("ae000003-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2352), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2353) },
                    { new Guid("ae000004-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2355), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2025, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2356) },
                    { new Guid("ae000005-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2358), "Ramazan Bayramı (3 gün)", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ramazan Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2359) },
                    { new Guid("ae000006-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2362), "30 Ağustos Zafer Bayramı", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2362) },
                    { new Guid("ae000007-0007-0007-0007-000000000007"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2365), "29 Ekim Cumhuriyet Bayramı", new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2365) },
                    { new Guid("ae000008-0008-0008-0008-000000000008"), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2371), "Kurban Bayramı (4 gün)", new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kurban Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2371) }
                });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(4945), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5023) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5095), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5096) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5099), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5099) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5102), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5102) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5105), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(5105) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9237), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9312) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9389), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9389) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9395), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9395) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9398), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9398) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9401), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9402) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9405), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(9405) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(6879), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(6953) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7039), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7039) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7042), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7043) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7045), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7045) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7048), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7048) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7050), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7051) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7053), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7054) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7056), new DateTime(2025, 12, 14, 11, 35, 13, 419, DateTimeKind.Utc).AddTicks(7056) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 359, DateTimeKind.Utc).AddTicks(5615), new DateTime(2025, 12, 14, 11, 35, 12, 359, DateTimeKind.Utc).AddTicks(5781) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 359, DateTimeKind.Utc).AddTicks(5946), new DateTime(2025, 12, 14, 11, 35, 12, 359, DateTimeKind.Utc).AddTicks(5947) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 359, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 12, 14, 11, 35, 12, 359, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(397), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(262), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(470) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(552), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(550), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(553) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(577), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(576), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(578) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(581), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(581), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(582) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(585), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(584), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(585) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(588), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(588), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(589) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(592), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(591), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(592) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(597), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(596), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(597) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(601), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(600), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(602) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(605), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(605), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(606) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(612), new DateTime(2025, 11, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(611), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(612) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 800, DateTimeKind.Utc).AddTicks(1867), new DateTime(2025, 12, 14, 11, 35, 12, 800, DateTimeKind.Utc).AddTicks(1948) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 800, DateTimeKind.Utc).AddTicks(2024), new DateTime(2025, 12, 14, 11, 35, 12, 800, DateTimeKind.Utc).AddTicks(2025) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8324), new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8412) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8534), new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8535) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8546), new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8546) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8550), new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8550) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8558), new DateTime(2025, 12, 14, 11, 35, 13, 417, DateTimeKind.Utc).AddTicks(8558) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 558, DateTimeKind.Utc).AddTicks(4242), "$2a$11$qBf/Due3asceNnzgeniZWOwZFHy.pHLbu.Y5xsek70vKgu1hW133q", new DateTime(2025, 12, 14, 11, 35, 12, 558, DateTimeKind.Utc).AddTicks(4462) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 919, DateTimeKind.Utc).AddTicks(7654), "$2a$11$vHLJesRe0XtSZoDT5S6ywu.ZsWxA9R5Yu6zYyyx1wFnmQdVbV1612", new DateTime(2025, 12, 14, 11, 35, 12, 919, DateTimeKind.Utc).AddTicks(7658) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 45, DateTimeKind.Utc).AddTicks(9708), "$2a$11$WvkpKBwLpDGVJKgNEdJ6FOHV6VBiIeczD4oGAFJuZTlCXrNaRK1xK", new DateTime(2025, 12, 14, 11, 35, 13, 45, DateTimeKind.Utc).AddTicks(9713) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 173, DateTimeKind.Utc).AddTicks(8088), "$2a$11$8j9F2K9E9l5mSLP191mvSeA15mieyQuLLtgroNTqz8FHgi6pxMQ3.", new DateTime(2025, 12, 14, 11, 35, 13, 173, DateTimeKind.Utc).AddTicks(8183) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 297, DateTimeKind.Utc).AddTicks(5237), "$2a$11$4iicdqvRZOQUOsIiVO809ePy2zaXwxUG5tFNLDlAD3rxlzFsrIiOC", new DateTime(2025, 12, 14, 11, 35, 13, 297, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 416, DateTimeKind.Utc).AddTicks(5747), "$2a$11$diFBSi5LAM7xKL0OGymEOeimUBZBoiAGhetGC2pL/I3YpRifgSmHO", new DateTime(2025, 12, 14, 11, 35, 13, 416, DateTimeKind.Utc).AddTicks(5751) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 680, DateTimeKind.Utc).AddTicks(136), "$2a$11$q/GCGC7ZfMR0LAL5jgWzm.nuTpNjBQQiuvkOJBEAdNvqj7WnyqwrW", new DateTime(2025, 12, 14, 11, 35, 12, 680, DateTimeKind.Utc).AddTicks(141) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 12, 799, DateTimeKind.Utc).AddTicks(8526), "$2a$11$WIMy/EKM/gwl/WBl0V5z8.0Q6rKUzNk/O6ai50N3e2P99VQnMCGtq", new DateTime(2025, 12, 14, 11, 35, 12, 799, DateTimeKind.Utc).AddTicks(8531) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"));

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
        }
    }
}
