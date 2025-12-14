using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceWarningEmailFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FailureEmailSent",
                table: "Enrollments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WarningEmailSent",
                table: "Enrollments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(235), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(91), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(303), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(377), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(375), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(377), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(380), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(381), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(384), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(383), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(384), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(389), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(389), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(390), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(393), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(392), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(393), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(396), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(395), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(396), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(399), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(399), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(399), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(402), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(402), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(403), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(406), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(405), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(406), false });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "FailureEmailSent", "UpdatedAt", "WarningEmailSent" },
                values: new object[] { new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(409), new DateTime(2025, 11, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(408), false, new DateTime(2025, 12, 14, 9, 14, 13, 14, DateTimeKind.Utc).AddTicks(409), false });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailureEmailSent",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "WarningEmailSent",
                table: "Enrollments");

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
        }
    }
}
