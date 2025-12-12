using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class Part2_AcademicAndAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Building = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    FeaturesJson = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    ECTS = table.Column<int>(type: "int", nullable: false),
                    SyllabusUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoursePrerequisites",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PrerequisiteCourseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrerequisites", x => new { x.CourseId, x.PrerequisiteCourseId });
                    table.ForeignKey(
                        name: "FK_CoursePrerequisites_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePrerequisites_Courses_PrerequisiteCourseId",
                        column: x => x.PrerequisiteCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CourseSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SectionNumber = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClassroomId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    EnrolledCount = table.Column<int>(type: "int", nullable: false),
                    ScheduleJson = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseSections_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CourseSections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSections_Faculties_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttendanceSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SectionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InstructorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    GeofenceRadius = table.Column<int>(type: "int", nullable: false),
                    QrCode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QrCodeExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceSessions_CourseSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "CourseSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceSessions_Faculties_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SectionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MidtermGrade = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    FinalGrade = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    HomeworkGrade = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    LetterGrade = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GradePoint = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_CourseSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "CourseSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttendanceRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CheckInTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    DistanceFromCenter = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Accuracy = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsFlagged = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FlagReason = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IpAddress = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAgent = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsQrVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceRecords_AttendanceSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "AttendanceSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExcuseRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Reason = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReviewedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ReviewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcuseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcuseRequests_AttendanceSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "AttendanceSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcuseRequests_Faculties_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExcuseRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "Building", "Capacity", "CreatedAt", "FeaturesJson", "IsActive", "Latitude", "Longitude", "RoomNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c1a00001-0001-0001-0001-000000000001"), "A Blok", 50, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(2842), "[\"projector\", \"whiteboard\", \"ac\", \"computer\"]", true, 41.1055m, 29.0250m, "101", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(2925) },
                    { new Guid("c2a00002-0002-0002-0002-000000000002"), "A Blok", 40, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3009), "[\"projector\", \"whiteboard\"]", true, 41.1056m, 29.0251m, "102", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3010) },
                    { new Guid("c3a00003-0003-0003-0003-000000000003"), "B Blok", 60, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3023), "[\"projector\", \"whiteboard\", \"ac\", \"lab_computers\"]", true, 41.1060m, 29.0255m, "201", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3024) },
                    { new Guid("c4a00004-0004-0004-0004-000000000004"), "B Blok", 35, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3027), "[\"projector\", \"whiteboard\", \"ac\"]", true, 41.1061m, 29.0256m, "202", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3027) },
                    { new Guid("c5a00005-0005-0005-0005-000000000005"), "C Blok", 30, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3029), "[\"projector\", \"whiteboard\", \"ac\", \"lab_computers\", \"network\"]", true, 41.1065m, 29.0260m, "Lab-1", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(3030) }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Code", "CreatedAt", "Credits", "DepartmentId", "Description", "ECTS", "IsActive", "Name", "SyllabusUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c0a00001-0001-0001-0001-000000000001"), "BM101", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(4859), 4, new Guid("11111111-1111-1111-1111-111111111111"), "Temel programlama kavramları, değişkenler, döngüler, fonksiyonlar ve nesne yönelimli programlamaya giriş.", 6, true, "Programlamaya Giriş", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(4964) },
                    { new Guid("c0a00002-0002-0002-0002-000000000002"), "BM201", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5041), 4, new Guid("11111111-1111-1111-1111-111111111111"), "Diziler, bağlı listeler, yığınlar, kuyruklar, ağaçlar ve graf veri yapıları.", 6, true, "Veri Yapıları", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5042) },
                    { new Guid("c0a00003-0003-0003-0003-000000000003"), "BM301", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5045), 3, new Guid("11111111-1111-1111-1111-111111111111"), "Algoritma analizi, sıralama ve arama algoritmaları, dinamik programlama, açgözlü algoritmalar.", 5, true, "Algoritmalar", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5046) },
                    { new Guid("c0a00004-0004-0004-0004-000000000004"), "BM302", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5048), 3, new Guid("11111111-1111-1111-1111-111111111111"), "İlişkisel veritabanları, SQL, normalizasyon, indeksleme ve transaction yönetimi.", 5, true, "Veritabanı Sistemleri", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5049) },
                    { new Guid("c0a00005-0005-0005-0005-000000000005"), "BM401", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5055), 3, new Guid("11111111-1111-1111-1111-111111111111"), "Yazılım geliştirme süreçleri, tasarım desenleri, test ve kalite güvencesi.", 5, true, "Yazılım Mühendisliği", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5055) },
                    { new Guid("c0a00006-0006-0006-0006-000000000006"), "BM402", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5057), 3, new Guid("11111111-1111-1111-1111-111111111111"), "HTML, CSS, JavaScript, React, Node.js ve modern web teknolojileri.", 5, true, "Web Programlama", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5058) },
                    { new Guid("c0a00007-0007-0007-0007-000000000007"), "EEM101", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5073), 4, new Guid("22222222-2222-2222-2222-222222222222"), "Temel elektrik devreleri, Kirchhoff yasaları, Thevenin ve Norton teoremleri.", 6, true, "Devre Analizi", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5074) },
                    { new Guid("c0a00008-0008-0008-0008-000000000008"), "EEM201", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5076), 4, new Guid("22222222-2222-2222-2222-222222222222"), "Diyotlar, transistörler, opamp'lar ve temel elektronik devreler.", 6, true, "Elektronik", null, new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(5077) }
                });

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

            migrationBuilder.InsertData(
                table: "CoursePrerequisites",
                columns: new[] { "CourseId", "PrerequisiteCourseId" },
                values: new object[,]
                {
                    { new Guid("c0a00002-0002-0002-0002-000000000002"), new Guid("c0a00001-0001-0001-0001-000000000001") },
                    { new Guid("c0a00003-0003-0003-0003-000000000003"), new Guid("c0a00002-0002-0002-0002-000000000002") },
                    { new Guid("c0a00004-0004-0004-0004-000000000004"), new Guid("c0a00002-0002-0002-0002-000000000002") },
                    { new Guid("c0a00005-0005-0005-0005-000000000005"), new Guid("c0a00003-0003-0003-0003-000000000003") },
                    { new Guid("c0a00005-0005-0005-0005-000000000005"), new Guid("c0a00004-0004-0004-0004-000000000004") },
                    { new Guid("c0a00006-0006-0006-0006-000000000006"), new Guid("c0a00001-0001-0001-0001-000000000001") },
                    { new Guid("c0a00008-0008-0008-0008-000000000008"), new Guid("c0a00007-0007-0007-0007-000000000007") }
                });

            migrationBuilder.InsertData(
                table: "CourseSections",
                columns: new[] { "Id", "Capacity", "ClassroomId", "CourseId", "CreatedAt", "EnrolledCount", "InstructorId", "IsActive", "ScheduleJson", "SectionNumber", "Semester", "UpdatedAt", "Year" },
                values: new object[,]
                {
                    { new Guid("5ec00001-0001-0001-0001-000000000001"), 50, new Guid("c1a00001-0001-0001-0001-000000000001"), new Guid("c0a00001-0001-0001-0001-000000000001"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7344), 3, new Guid("fa111111-1111-1111-1111-111111111111"), true, "[{\"day\":\"Monday\",\"startTime\":\"09:00\",\"endTime\":\"10:50\"},{\"day\":\"Wednesday\",\"startTime\":\"09:00\",\"endTime\":\"10:50\"}]", 1, "Fall", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7423), 2025 },
                    { new Guid("5ec00002-0002-0002-0002-000000000002"), 40, new Guid("c2a00002-0002-0002-0002-000000000002"), new Guid("c0a00002-0002-0002-0002-000000000002"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7501), 2, new Guid("fa111111-1111-1111-1111-111111111111"), true, "[{\"day\":\"Tuesday\",\"startTime\":\"11:00\",\"endTime\":\"12:50\"},{\"day\":\"Thursday\",\"startTime\":\"11:00\",\"endTime\":\"12:50\"}]", 1, "Fall", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7502), 2025 },
                    { new Guid("5ec00003-0003-0003-0003-000000000003"), 35, new Guid("c3a00003-0003-0003-0003-000000000003"), new Guid("c0a00003-0003-0003-0003-000000000003"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7505), 2, new Guid("fa111111-1111-1111-1111-111111111111"), true, "[{\"day\":\"Monday\",\"startTime\":\"13:00\",\"endTime\":\"14:50\"},{\"day\":\"Wednesday\",\"startTime\":\"13:00\",\"endTime\":\"14:50\"}]", 1, "Fall", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7506), 2025 },
                    { new Guid("5ec00004-0004-0004-0004-000000000004"), 35, new Guid("c4a00004-0004-0004-0004-000000000004"), new Guid("c0a00004-0004-0004-0004-000000000004"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7512), 1, new Guid("fa111111-1111-1111-1111-111111111111"), true, "[{\"day\":\"Tuesday\",\"startTime\":\"14:00\",\"endTime\":\"15:50\"},{\"day\":\"Thursday\",\"startTime\":\"14:00\",\"endTime\":\"15:50\"}]", 1, "Fall", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7512), 2025 },
                    { new Guid("5ec00005-0005-0005-0005-000000000005"), 30, new Guid("c5a00005-0005-0005-0005-000000000005"), new Guid("c0a00006-0006-0006-0006-000000000006"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7530), 2, new Guid("fa111111-1111-1111-1111-111111111111"), true, "[{\"day\":\"Friday\",\"startTime\":\"10:00\",\"endTime\":\"12:50\"}]", 1, "Fall", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7531), 2025 },
                    { new Guid("5ec00006-0006-0006-0006-000000000006"), 45, new Guid("c1a00001-0001-0001-0001-000000000001"), new Guid("c0a00007-0007-0007-0007-000000000007"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7534), 1, new Guid("fa222222-2222-2222-2222-222222222222"), true, "[{\"day\":\"Monday\",\"startTime\":\"15:00\",\"endTime\":\"16:50\"},{\"day\":\"Wednesday\",\"startTime\":\"15:00\",\"endTime\":\"16:50\"}]", 1, "Fall", new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(7534), 2025 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CreatedAt", "EnrollmentDate", "FinalGrade", "GradePoint", "HomeworkGrade", "LetterGrade", "MidtermGrade", "SectionId", "Status", "StudentId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("e0a00001-0001-0001-0001-000000000001"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8443), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8297), null, null, null, null, null, new Guid("5ec00001-0001-0001-0001-000000000001"), "Active", new Guid("d1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8516) },
                    { new Guid("e0a00002-0002-0002-0002-000000000002"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8592), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8590), null, null, null, null, null, new Guid("5ec00002-0002-0002-0002-000000000002"), "Active", new Guid("d1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8592) },
                    { new Guid("e0a00003-0003-0003-0003-000000000003"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8597), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8596), null, null, null, null, null, new Guid("5ec00003-0003-0003-0003-000000000003"), "Active", new Guid("d1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8597) },
                    { new Guid("e0a00004-0004-0004-0004-000000000004"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8600), null, null, null, null, null, new Guid("5ec00001-0001-0001-0001-000000000001"), "Active", new Guid("d2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8601) },
                    { new Guid("e0a00005-0005-0005-0005-000000000005"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8604), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8603), null, null, null, null, null, new Guid("5ec00004-0004-0004-0004-000000000004"), "Active", new Guid("d2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8604) },
                    { new Guid("e0a00006-0006-0006-0006-000000000006"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8607), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8607), null, null, null, null, null, new Guid("5ec00002-0002-0002-0002-000000000002"), "Active", new Guid("d3333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8607) },
                    { new Guid("e0a00007-0007-0007-0007-000000000007"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8613), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8612), null, null, null, null, null, new Guid("5ec00003-0003-0003-0003-000000000003"), "Active", new Guid("d3333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8613) },
                    { new Guid("e0a00008-0008-0008-0008-000000000008"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8616), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8616), null, null, null, null, null, new Guid("5ec00005-0005-0005-0005-000000000005"), "Active", new Guid("d3333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8616) },
                    { new Guid("e0a00009-0009-0009-0009-000000000009"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8619), null, null, null, null, null, new Guid("5ec00001-0001-0001-0001-000000000001"), "Active", new Guid("d4444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8620) },
                    { new Guid("e0a00010-0010-0010-0010-000000000010"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8623), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8622), null, null, null, null, null, new Guid("5ec00005-0005-0005-0005-000000000005"), "Active", new Guid("d4444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8623) },
                    { new Guid("e0a00011-0011-0011-0011-000000000011"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8626), new DateTime(2025, 11, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8626), null, null, null, null, null, new Guid("5ec00006-0006-0006-0006-000000000006"), "Active", new Guid("d5555555-5555-5555-5555-555555555555"), new DateTime(2025, 12, 12, 19, 57, 30, 1, DateTimeKind.Utc).AddTicks(8626) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecords_SessionId_StudentId",
                table: "AttendanceRecords",
                columns: new[] { "SessionId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecords_StudentId",
                table: "AttendanceRecords",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSessions_InstructorId",
                table: "AttendanceSessions",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSessions_SectionId_Date",
                table: "AttendanceSessions",
                columns: new[] { "SectionId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Building_RoomNumber",
                table: "Classrooms",
                columns: new[] { "Building", "RoomNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrerequisites_PrerequisiteCourseId",
                table: "CoursePrerequisites",
                column: "PrerequisiteCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Code",
                table: "Courses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_ClassroomId",
                table: "CourseSections",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_CourseId_SectionNumber_Semester_Year",
                table: "CourseSections",
                columns: new[] { "CourseId", "SectionNumber", "Semester", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_InstructorId",
                table: "CourseSections",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SectionId",
                table: "Enrollments",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId_SectionId",
                table: "Enrollments",
                columns: new[] { "StudentId", "SectionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExcuseRequests_ReviewedBy",
                table: "ExcuseRequests",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExcuseRequests_SessionId",
                table: "ExcuseRequests",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcuseRequests_StudentId_SessionId",
                table: "ExcuseRequests",
                columns: new[] { "StudentId", "SessionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceRecords");

            migrationBuilder.DropTable(
                name: "CoursePrerequisites");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "ExcuseRequests");

            migrationBuilder.DropTable(
                name: "AttendanceSessions");

            migrationBuilder.DropTable(
                name: "CourseSections");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 56, 901, DateTimeKind.Utc).AddTicks(548), new DateTime(2025, 12, 10, 14, 9, 56, 901, DateTimeKind.Utc).AddTicks(629) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 56, 901, DateTimeKind.Utc).AddTicks(703), new DateTime(2025, 12, 10, 14, 9, 56, 901, DateTimeKind.Utc).AddTicks(703) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 56, 901, DateTimeKind.Utc).AddTicks(706), new DateTime(2025, 12, 10, 14, 9, 56, 901, DateTimeKind.Utc).AddTicks(706) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(3213), new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(3311) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(3392), new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(3392) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3141), new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3231) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3338), new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3339) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3343), new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3343) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3346), new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3347) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 12, 10, 14, 9, 57, 866, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 63, DateTimeKind.Utc).AddTicks(1617), "$2a$11$woFIq/nSuIJ1dYfnDufiCucXwo.ToSF2vpBpxBJWNRT9e9DcTedQG", new DateTime(2025, 12, 10, 14, 9, 57, 63, DateTimeKind.Utc).AddTicks(1934) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 409, DateTimeKind.Utc).AddTicks(2865), "$2a$11$MT0NmRu/wWSYslsFuz6.feJf90.HpoenGX5tZv83CiYhS1Eo4BN7q", new DateTime(2025, 12, 10, 14, 9, 57, 409, DateTimeKind.Utc).AddTicks(2869) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 523, DateTimeKind.Utc).AddTicks(6899), "$2a$11$qXIkjZtb.R3uUMiIIc.1fOTQCF45hFnU9vNnETcPA0JAifYitkkH6", new DateTime(2025, 12, 10, 14, 9, 57, 523, DateTimeKind.Utc).AddTicks(6904) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 637, DateTimeKind.Utc).AddTicks(4765), "$2a$11$o5KW4kFNCC3XIRAtyz42SeMkEuhQiIs9FwRvzrAXg35CzK06/.JXG", new DateTime(2025, 12, 10, 14, 9, 57, 637, DateTimeKind.Utc).AddTicks(4832) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 751, DateTimeKind.Utc).AddTicks(3849), "$2a$11$pTSdtF2u3tcSwyCQhpyQ5uyJgYWnb6w9qc7pUr7Ry8dZtI8xHP9Ry", new DateTime(2025, 12, 10, 14, 9, 57, 751, DateTimeKind.Utc).AddTicks(3852) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 865, DateTimeKind.Utc).AddTicks(987), "$2a$11$U/b4s4nRXieLT6URFbA/Xuslx.d86inqqNB0967QofzSVVTHj8KjS", new DateTime(2025, 12, 10, 14, 9, 57, 865, DateTimeKind.Utc).AddTicks(990) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 180, DateTimeKind.Utc).AddTicks(8720), "$2a$11$xuDSEPXho1DbcmX6fSjMy.jJdERxXSPj2p//evBNcWyjH4G8pBf5C", new DateTime(2025, 12, 10, 14, 9, 57, 180, DateTimeKind.Utc).AddTicks(8725) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(314), "$2a$11$9Tq9jVGlAKAUTbQqCo2/meXzDXjrr6ajMNWMA8HUaOv.CFv5O/61C", new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(318) });
        }
    }
}
