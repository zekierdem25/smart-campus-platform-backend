using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPart3Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cafeterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafeterias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClassroomReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClassroomId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Purpose = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApprovedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApprovedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassroomReservations_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassroomReservations_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ClassroomReservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Location = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    RegisteredCount = table.Column<int>(type: "int", nullable: false),
                    RegistrationDeadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsPaid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SectionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DayOfWeek = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Semester = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_CourseSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "CourseSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Balance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Currency = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MealMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CafeteriaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MealType = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemsJson = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NutritionJson = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealMenus_Cafeterias_CafeteriaId",
                        column: x => x.CafeteriaId,
                        principalTable: "Cafeterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    QrCode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CheckedIn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CheckedInAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CustomFieldsJson = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WalletId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ReferenceType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReferenceId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MealReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MenuId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CafeteriaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MealType = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QrCode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealReservations_Cafeterias_CafeteriaId",
                        column: x => x.CafeteriaId,
                        principalTable: "Cafeterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MealReservations_MealMenus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "MealMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MealReservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4493), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4493) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4493), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4493) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000011-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000012-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000013-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000014-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000015-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000016-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000017-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4645), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4645) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000018-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4645), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4645) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000021-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000022-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000023-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000024-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000025-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000026-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000027-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4685), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4685) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000028-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4685), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4685) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000031-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000032-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000033-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000034-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000035-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000036-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000037-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4737), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4737) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000038-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4737), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4737) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000041-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000042-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000043-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000044-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000045-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000046-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000047-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000048-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000051-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000052-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000053-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000054-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000055-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000056-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000057-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4793), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4793) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000058-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4793), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4793) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000061-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000062-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000063-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000064-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000065-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000066-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000067-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4815), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4815) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000068-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4815), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4815) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000071-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000072-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000073-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000074-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000075-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000076-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000077-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4844), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4844) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000078-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4844), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4844) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000081-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000082-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000083-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000084-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000085-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000086-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000087-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4866), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4866) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000088-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4866), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4866) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000091-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000092-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000093-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000094-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000095-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000096-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000097-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4896), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4896) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000098-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4896), new DateTime(2025, 12, 20, 8, 1, 19, 191, DateTimeKind.Utc).AddTicks(4896) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3532), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3637) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3722), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3722) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3725), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3725) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3728), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3728) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3737), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(3737) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8049), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8126) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8238), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8239) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8243) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8259), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8259) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8262), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8262) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8265), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(8266) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5521), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5596) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5673), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5673) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5676), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5677) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5679), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5682), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5682) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5698), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5699) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5701), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5702) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5708), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(5708) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 138, DateTimeKind.Utc).AddTicks(2404), new DateTime(2025, 12, 20, 8, 1, 18, 138, DateTimeKind.Utc).AddTicks(2502) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 138, DateTimeKind.Utc).AddTicks(2596), new DateTime(2025, 12, 20, 8, 1, 18, 138, DateTimeKind.Utc).AddTicks(2597) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 138, DateTimeKind.Utc).AddTicks(2600), new DateTime(2025, 12, 20, 8, 1, 18, 138, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9186), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9046), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9254) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9333), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9331), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9333) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9337), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9336), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9343), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9343), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9344) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9347), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9346), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9359) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9363), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9362), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9363) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9366), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9365), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9366) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9369), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9369), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9374), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9374), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9374) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9377), new DateTime(2025, 11, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9377), new DateTime(2025, 12, 20, 8, 1, 19, 189, DateTimeKind.Utc).AddTicks(9378) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 610, DateTimeKind.Utc).AddTicks(8672), new DateTime(2025, 12, 20, 8, 1, 18, 610, DateTimeKind.Utc).AddTicks(8757) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 610, DateTimeKind.Utc).AddTicks(8835), new DateTime(2025, 12, 20, 8, 1, 18, 610, DateTimeKind.Utc).AddTicks(8836) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(5784), new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(5871) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(6005), new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(6005) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(6009), new DateTime(2025, 12, 20, 8, 1, 19, 187, DateTimeKind.Utc).AddTicks(6009) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 339, DateTimeKind.Utc).AddTicks(7431), "$2a$11$oH762wl/MCmDcpKb.M/DGOM4/tYpVnBCGzqxze1shVTPbTHmsYWsq", new DateTime(2025, 12, 20, 8, 1, 18, 339, DateTimeKind.Utc).AddTicks(7587) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 724, DateTimeKind.Utc).AddTicks(6838), "$2a$11$Up8vTOsXehO7oCxPcC1JxeyHkPN5yHCDBfXmcjy6PViONA04u.IW.", new DateTime(2025, 12, 20, 8, 1, 18, 724, DateTimeKind.Utc).AddTicks(6842) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 840, DateTimeKind.Utc).AddTicks(5177), "$2a$11$lxuthamlSqu/pvfVz0oJouH4VvH7R9fv6BLXY0iWfHxGhImiTpRqu", new DateTime(2025, 12, 20, 8, 1, 18, 840, DateTimeKind.Utc).AddTicks(5181) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 955, DateTimeKind.Utc).AddTicks(4737), "$2a$11$bJQDcchSG0I.xso7rrWeIO2K1IcDILWXpcKp2R3.KtWLQauOUwhyy", new DateTime(2025, 12, 20, 8, 1, 18, 955, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 70, DateTimeKind.Utc).AddTicks(5344), "$2a$11$EW3tDOmLzwMZFNlwsioKuueiCufJLvnuwzLjxxf/cxAFX7OLqR.Ui", new DateTime(2025, 12, 20, 8, 1, 19, 70, DateTimeKind.Utc).AddTicks(5409) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 19, 186, DateTimeKind.Utc).AddTicks(3266), "$2a$11$BxeSUamNthlRju1unk06G.daUNZasi4jMTaAsJVsVvIOSEUQLigae", new DateTime(2025, 12, 20, 8, 1, 19, 186, DateTimeKind.Utc).AddTicks(3268) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 497, DateTimeKind.Utc).AddTicks(1350), "$2a$11$anEQimPtf/bm5Tt7NPo7b.uzIN1qVs94m0dCjHS.2xqo0wcb7Ts7C", new DateTime(2025, 12, 20, 8, 1, 18, 497, DateTimeKind.Utc).AddTicks(1374) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 1, 18, 610, DateTimeKind.Utc).AddTicks(5232), "$2a$11$tC0SBUB.I047/9.cGo3KEuLpExxwqFrFm6wPHRoGAo/ltWzfbVdmO", new DateTime(2025, 12, 20, 8, 1, 18, 610, DateTimeKind.Utc).AddTicks(5236) });

            migrationBuilder.CreateIndex(
                name: "IX_Cafeterias_Name",
                table: "Cafeterias",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomReservations_ApprovedBy",
                table: "ClassroomReservations",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomReservations_ClassroomId_Date_StartTime",
                table: "ClassroomReservations",
                columns: new[] { "ClassroomId", "Date", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomReservations_UserId",
                table: "ClassroomReservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_EventId_UserId",
                table: "EventRegistrations",
                columns: new[] { "EventId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_QrCode",
                table: "EventRegistrations",
                column: "QrCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_UserId",
                table: "EventRegistrations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatedBy",
                table: "Events",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Date_Status",
                table: "Events",
                columns: new[] { "Date", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_MealMenus_CafeteriaId_Date_MealType",
                table: "MealMenus",
                columns: new[] { "CafeteriaId", "Date", "MealType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealReservations_CafeteriaId",
                table: "MealReservations",
                column: "CafeteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MealReservations_MenuId",
                table: "MealReservations",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MealReservations_QrCode",
                table: "MealReservations",
                column: "QrCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealReservations_UserId_Date_MealType",
                table: "MealReservations",
                columns: new[] { "UserId", "Date", "MealType" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassroomId_DayOfWeek_StartTime_Semester_Year",
                table: "Schedules",
                columns: new[] { "ClassroomId", "DayOfWeek", "StartTime", "Semester", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SectionId_DayOfWeek_Semester_Year",
                table: "Schedules",
                columns: new[] { "SectionId", "DayOfWeek", "Semester", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId_CreatedAt",
                table: "Transactions",
                columns: new[] { "WalletId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassroomReservations");

            migrationBuilder.DropTable(
                name: "EventRegistrations");

            migrationBuilder.DropTable(
                name: "MealReservations");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "MealMenus");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Cafeterias");

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5498), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5498) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5498), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5498) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000011-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000012-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000013-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000014-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000015-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000016-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000017-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5727), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5727) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000018-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5727), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5727) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000021-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000022-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000023-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000024-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000025-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000026-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000027-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5788), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5788) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000028-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5788), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5788) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000031-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000032-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000033-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000034-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000035-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000036-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000037-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000038-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000041-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000042-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000043-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000044-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000045-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000046-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000047-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5866), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5866) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000048-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5866), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5866) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000051-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000052-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000053-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000054-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000055-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000056-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000057-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5909), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5909) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000058-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5909), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5909) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000061-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000062-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000063-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000064-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000065-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000066-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000067-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000068-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000071-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000072-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000073-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000074-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000075-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000076-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000077-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5963), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5963) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000078-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5963), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5963) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000081-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000082-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000083-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000084-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000085-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000086-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000087-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5994), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5994) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000088-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5994), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(5994) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000091-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000092-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000093-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000094-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000095-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000096-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3938) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000097-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(6021), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(6021) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000098-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(6021), new DateTime(2025, 12, 14, 12, 9, 50, 870, DateTimeKind.Utc).AddTicks(6021) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5063) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5156), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5156) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5160), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5160) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5183), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(5183) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2202), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2281) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2364), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2365) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2374), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2375) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2387), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2388) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2406), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2406) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8586), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8671) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8775), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8776) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8779), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8779) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8793), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8794) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8796), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8797) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8799), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8803), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8803) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8840), new DateTime(2025, 12, 14, 12, 9, 50, 867, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 49, 808, DateTimeKind.Utc).AddTicks(2965), new DateTime(2025, 12, 14, 12, 9, 49, 808, DateTimeKind.Utc).AddTicks(3283) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 49, 808, DateTimeKind.Utc).AddTicks(3651), new DateTime(2025, 12, 14, 12, 9, 49, 808, DateTimeKind.Utc).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 49, 808, DateTimeKind.Utc).AddTicks(3689), new DateTime(2025, 12, 14, 12, 9, 49, 808, DateTimeKind.Utc).AddTicks(3690) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3288), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3606) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3685), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3683), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3687) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3691), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3690), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3691) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3694), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3694), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3695) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3698), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3697), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3698) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3705), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3704), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3705) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3708), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3708), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3708) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3711), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3711), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3712) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3729), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3729), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3733), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3733), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3733) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3737), new DateTime(2025, 11, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3736), new DateTime(2025, 12, 14, 12, 9, 50, 868, DateTimeKind.Utc).AddTicks(3737) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 250, DateTimeKind.Utc).AddTicks(5664), new DateTime(2025, 12, 14, 12, 9, 50, 250, DateTimeKind.Utc).AddTicks(5761) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 250, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 12, 14, 12, 9, 50, 250, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(1841), new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(1945) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2087), new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2087) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2092), new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2092) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2097), new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2098) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2118), new DateTime(2025, 12, 14, 12, 9, 50, 865, DateTimeKind.Utc).AddTicks(2119) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 3, DateTimeKind.Utc).AddTicks(8608), "$2a$11$Z.TiFqmq8dqW9CAFrKdo0.v.qkVikfmQGoZSXpXnqov1Yuu5G2M9e", new DateTime(2025, 12, 14, 12, 9, 50, 3, DateTimeKind.Utc).AddTicks(8777) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 371, DateTimeKind.Utc).AddTicks(8090), "$2a$11$kep5IGKqSeZj..7BRxbXWuSPZo3Psn6UWo5uPhsH7wdHfsZHp./Sq", new DateTime(2025, 12, 14, 12, 9, 50, 371, DateTimeKind.Utc).AddTicks(8094) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 492, DateTimeKind.Utc).AddTicks(1559), "$2a$11$J1biwCCvq7QyJvJkaFGYWea.PARJ23AuGWnrr6w2UbZx1niCeaFtC", new DateTime(2025, 12, 14, 12, 9, 50, 492, DateTimeKind.Utc).AddTicks(1565) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 612, DateTimeKind.Utc).AddTicks(9470), "$2a$11$FBOM4MEJJr4CgQoWZbazn.PHceHXnniwKdrTHbkUpBfGyjK6u9oMG", new DateTime(2025, 12, 14, 12, 9, 50, 612, DateTimeKind.Utc).AddTicks(9552) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 744, DateTimeKind.Utc).AddTicks(3377), "$2a$11$aJhLB/vfjlBWxkZlHGp9UOnK//McKX6nMY4ffe8s9vvMqZKWkb7WW", new DateTime(2025, 12, 14, 12, 9, 50, 744, DateTimeKind.Utc).AddTicks(3380) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 863, DateTimeKind.Utc).AddTicks(4213), "$2a$11$qJARs6rDC03PUmyXavrOsuEdO2X.Nc6ef5EsvF.kLcSFTGWxlWCLq", new DateTime(2025, 12, 14, 12, 9, 50, 863, DateTimeKind.Utc).AddTicks(4216) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 129, DateTimeKind.Utc).AddTicks(218), "$2a$11$m55I2mOTS10j6pGMA8ZvNOKKzHI6OmFvXnHTIj1GWBP8dPuUID2fy", new DateTime(2025, 12, 14, 12, 9, 50, 129, DateTimeKind.Utc).AddTicks(224) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 12, 9, 50, 250, DateTimeKind.Utc).AddTicks(1824), "$2a$11$Di7.KfFYucDQl5nA2ePJ1OJVcaDxG7QHyGzHB7elNDkj8LCfG1Yly", new DateTime(2025, 12, 14, 12, 9, 50, 250, DateTimeKind.Utc).AddTicks(1829) });
        }
    }
}
