using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Part3FeaturesAddSurveyAndEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequirementsJson",
                table: "Courses",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventSurveys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchemaJson = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndsAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSurveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSurveys_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventWaitlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NotifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventWaitlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventWaitlists_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventWaitlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PendingPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Provider = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingPayments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipmentBorrowings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EquipmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BorrowDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpectedReturnDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActualReturnDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purpose = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApprovedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApprovedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentBorrowings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentBorrowings_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentBorrowings_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EquipmentBorrowings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventSurveyResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SurveyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResponsesJson = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmittedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSurveyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSurveyResponses_EventSurveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "EventSurveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSurveyResponses_Users_UserId",
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
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7763), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7763) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7763), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7763) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000011-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000012-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000013-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000014-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000015-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000016-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000017-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7857), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7857) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000018-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7857), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7857) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000021-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000022-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000023-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000024-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000025-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000026-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000027-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7891), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7891) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000028-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7891), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7891) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000031-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000032-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000033-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000034-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000035-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000036-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000037-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7928), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7928) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000038-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7928), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7928) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000041-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000042-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000043-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000044-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000045-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000046-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000047-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7969), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7969) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000048-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7969), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7969) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000051-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000052-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000053-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000054-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000055-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000056-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000057-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7990), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000058-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7990), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000061-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000062-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000063-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000064-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000065-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000066-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000067-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8011), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8011) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000068-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8011), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8011) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000071-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000072-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000073-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000074-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000075-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000076-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000077-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8031), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8031) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000078-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8031), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8031) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000081-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000082-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000083-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000084-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000085-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000086-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000087-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8066), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8066) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000088-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8066), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8066) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000091-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000092-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000093-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000094-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000095-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000096-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000097-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8087), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8087) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000098-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8087), new DateTime(2025, 12, 20, 11, 17, 39, 445, DateTimeKind.Utc).AddTicks(8087) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(4991), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(5103) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(5181), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(5181) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7748), new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7826) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7900), new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7900) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7903), new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7904) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7907), new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7907) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7910), new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(7910) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2502), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2665), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2665) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2675), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2675) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2678), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2679) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2681), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2682) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2685), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(2685) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(9796), null, new DateTime(2025, 12, 20, 11, 17, 39, 443, DateTimeKind.Utc).AddTicks(9972) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(74), null, new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(74) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(84), null, new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(84) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(98), null, new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(98) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(104), null, new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(105) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(109), null, new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(109) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(113), null, new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(113) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "RequirementsJson", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(116), null, new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(116) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 471, DateTimeKind.Utc).AddTicks(7262), new DateTime(2025, 12, 20, 11, 17, 38, 471, DateTimeKind.Utc).AddTicks(7353) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 471, DateTimeKind.Utc).AddTicks(7432), new DateTime(2025, 12, 20, 11, 17, 38, 471, DateTimeKind.Utc).AddTicks(7433) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 471, DateTimeKind.Utc).AddTicks(7436), new DateTime(2025, 12, 20, 11, 17, 38, 471, DateTimeKind.Utc).AddTicks(7436) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3586), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3446), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3654) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3728), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3725), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3728) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3732), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3732), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3733) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3736), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3735), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3736) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3755), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3754), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3758), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3758), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3759) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3778), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3777), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3778) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3781), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3781), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3781) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3784), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3784), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3785) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3788), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3787), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3788) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3791), new DateTime(2025, 11, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3790), new DateTime(2025, 12, 20, 11, 17, 39, 444, DateTimeKind.Utc).AddTicks(3791) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(6997), new DateTime(2026, 1, 14, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(6656), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(7064) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(7139), new DateTime(2025, 12, 24, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(7138), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(7139) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(7396), new DateTime(2025, 12, 28, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(7396) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 871, DateTimeKind.Utc).AddTicks(1981), new DateTime(2025, 12, 20, 11, 17, 38, 871, DateTimeKind.Utc).AddTicks(2066) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 871, DateTimeKind.Utc).AddTicks(2141), new DateTime(2025, 12, 20, 11, 17, 38, 871, DateTimeKind.Utc).AddTicks(2141) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(8537), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(8608) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(8685), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(8686) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(8691), new DateTime(2025, 12, 20, 11, 17, 39, 446, DateTimeKind.Utc).AddTicks(8691) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8768), new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8852) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8965), new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8966) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8971), new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8971) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8976), new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8976) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8999), new DateTime(2025, 12, 20, 11, 17, 39, 441, DateTimeKind.Utc).AddTicks(8999) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 643, DateTimeKind.Utc).AddTicks(8329), "$2a$11$K0O0VOKRV7U8yGQlZoyfDu42kR8BO4D8mT5RyuaZ0EX59Ts6T1BLu", new DateTime(2025, 12, 20, 11, 17, 38, 643, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 985, DateTimeKind.Utc).AddTicks(9996), "$2a$11$N61XmEMs/.qWypHYuqB0H.8iUdjSQ9PMm39D/Oqzy9bGHqw1ot4dC", new DateTime(2025, 12, 20, 11, 17, 38, 985, DateTimeKind.Utc).AddTicks(9999) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 99, DateTimeKind.Utc).AddTicks(2033), "$2a$11$QSBfLHoQ0mSfGwWQyG5vz.ebXjmVg47AqH5CiEmTdR8YLNTmyly5y", new DateTime(2025, 12, 20, 11, 17, 39, 99, DateTimeKind.Utc).AddTicks(2038) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 214, DateTimeKind.Utc).AddTicks(127), "$2a$11$QYVrYCjmNXpvDQr5X3Uq1.eieI1GL7hQCXAJAVGzHYW6iff.zkGtW", new DateTime(2025, 12, 20, 11, 17, 39, 214, DateTimeKind.Utc).AddTicks(191) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 327, DateTimeKind.Utc).AddTicks(4396), "$2a$11$AIl3.R6cp8QiRr2VJ50Ppu21XgjMYnyeyI/YkTXyvboQwfAUIXgtG", new DateTime(2025, 12, 20, 11, 17, 39, 327, DateTimeKind.Utc).AddTicks(4401) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 39, 440, DateTimeKind.Utc).AddTicks(6021), "$2a$11$Qytgg9OdGwv7uM1UoRyRZOAoGugMATP11XK0g8KBmwpZ5ILAVUpSy", new DateTime(2025, 12, 20, 11, 17, 39, 440, DateTimeKind.Utc).AddTicks(6024) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 758, DateTimeKind.Utc).AddTicks(4424), "$2a$11$D0qQi2JxCFtqm6oBpy4MWeyYknrc5pZEEOTPT3wRlvHEqjHK38TFm", new DateTime(2025, 12, 20, 11, 17, 38, 758, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 11, 17, 38, 870, DateTimeKind.Utc).AddTicks(8735), "$2a$11$tQNytDYcFRQEpXypMEy2r.EBD8gOMBlujv1aLBy5PYUss91vTj7a6", new DateTime(2025, 12, 20, 11, 17, 38, 870, DateTimeKind.Utc).AddTicks(8744) });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBorrowings_ApprovedBy",
                table: "EquipmentBorrowings",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBorrowings_EquipmentId_Status",
                table: "EquipmentBorrowings",
                columns: new[] { "EquipmentId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBorrowings_UserId_Status",
                table: "EquipmentBorrowings",
                columns: new[] { "UserId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_SerialNumber",
                table: "Equipments",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Type_Status",
                table: "Equipments",
                columns: new[] { "Type", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveyResponses_SurveyId_UserId",
                table: "EventSurveyResponses",
                columns: new[] { "SurveyId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveyResponses_UserId",
                table: "EventSurveyResponses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveys_EventId_IsActive",
                table: "EventSurveys",
                columns: new[] { "EventId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_EventWaitlists_EventId_Position",
                table: "EventWaitlists",
                columns: new[] { "EventId", "Position" });

            migrationBuilder.CreateIndex(
                name: "IX_EventWaitlists_EventId_UserId",
                table: "EventWaitlists",
                columns: new[] { "EventId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventWaitlists_UserId",
                table: "EventWaitlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingPayments_SessionId",
                table: "PendingPayments",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PendingPayments_UserId_Status",
                table: "PendingPayments",
                columns: new[] { "UserId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentBorrowings");

            migrationBuilder.DropTable(
                name: "EventSurveyResponses");

            migrationBuilder.DropTable(
                name: "EventWaitlists");

            migrationBuilder.DropTable(
                name: "PendingPayments");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "EventSurveys");

            migrationBuilder.DropColumn(
                name: "RequirementsJson",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(1970), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(1970) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(1970), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(1970) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000011-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000012-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000013-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000014-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000015-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000016-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000017-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2111), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2111) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000018-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2111), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2111) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000021-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000022-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000023-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000024-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000025-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000026-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000027-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2159), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2159) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000028-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2159), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2159) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000031-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000032-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000033-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000034-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000035-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000036-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000037-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2198), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2198) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000038-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2198), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2198) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000041-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000042-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000043-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000044-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000045-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000046-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000047-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2222), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2222) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000048-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2222), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2222) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000051-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000052-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000053-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000054-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000055-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000056-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000057-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2256), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2256) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000058-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2256), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2256) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000061-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000062-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000063-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000064-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000065-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000066-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000067-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2279), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2279) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000068-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2279), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2279) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000071-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000072-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000073-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000074-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000075-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000076-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000077-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2306), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2306) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000078-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2306), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2306) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000081-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000082-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000083-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000084-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000085-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000086-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000087-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000088-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000091-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000092-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000093-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000094-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000095-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000096-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000097-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2358), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2358) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000098-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2358), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(2358) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9671), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9762) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9840), new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9840) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(8931), new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9016) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9105), new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9105) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9108), new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9109) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9112), new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9113) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9124), new DateTime(2025, 12, 20, 8, 34, 14, 904, DateTimeKind.Utc).AddTicks(9124) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5521), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5725), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5725) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5729), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5729) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5748), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5748) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5754), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5754) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5776), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(5776) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2394), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2477) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2565), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2565) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2569), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2569) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2572), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2572) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2575), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2575) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2633), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2634) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2637), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2637) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 13, 805, DateTimeKind.Utc).AddTicks(9181), new DateTime(2025, 12, 20, 8, 34, 13, 805, DateTimeKind.Utc).AddTicks(9276) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 13, 805, DateTimeKind.Utc).AddTicks(9366), new DateTime(2025, 12, 20, 8, 34, 13, 805, DateTimeKind.Utc).AddTicks(9367) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 13, 805, DateTimeKind.Utc).AddTicks(9370), new DateTime(2025, 12, 20, 8, 34, 13, 805, DateTimeKind.Utc).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6764), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6584), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6842) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6921), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6918), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6921) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6928), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6928), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6929) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6932), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6932), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6933) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6936), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6935), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6936) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6940), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6939), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6952) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6955), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6955), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6956) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6959), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6959), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6960) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6963), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6962), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6963) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6967), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6966), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6967) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6972), new DateTime(2025, 11, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6972), new DateTime(2025, 12, 20, 8, 34, 14, 905, DateTimeKind.Utc).AddTicks(6972) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(1961), new DateTime(2026, 1, 14, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(1593), new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2034) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2114), new DateTime(2025, 12, 24, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2113), new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2115) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2409), new DateTime(2025, 12, 28, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2120), new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2409) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 305, DateTimeKind.Utc).AddTicks(6305), new DateTime(2025, 12, 20, 8, 34, 14, 305, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 305, DateTimeKind.Utc).AddTicks(6491), new DateTime(2025, 12, 20, 8, 34, 14, 305, DateTimeKind.Utc).AddTicks(6491) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3637), new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3709) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3782), new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3782) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3792), new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3793) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(7872), new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(7975) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8137), new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8139) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8154), new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8154) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8162), new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8162) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8166), new DateTime(2025, 12, 20, 8, 34, 14, 902, DateTimeKind.Utc).AddTicks(8166) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 40, DateTimeKind.Utc).AddTicks(3294), "$2a$11$W1CMKnieFmueffhaDY.8YOkUmzcmSnvOG0hNSy0e//YtH/8eJCJm2", new DateTime(2025, 12, 20, 8, 34, 14, 40, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 430, DateTimeKind.Utc).AddTicks(5209), "$2a$11$dBzS074yltAVy4ct7iZfaO7TIafcV4Z8lAfYn.0rfmRCc1Ej2VtAG", new DateTime(2025, 12, 20, 8, 34, 14, 430, DateTimeKind.Utc).AddTicks(5214) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 548, DateTimeKind.Utc).AddTicks(6358), "$2a$11$YhPXv5/k3bzq0Fa2mhOUVeKtlIokr4Gn3XD6VuBi7cQ6Rm70RZWvG", new DateTime(2025, 12, 20, 8, 34, 14, 548, DateTimeKind.Utc).AddTicks(6363) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 665, DateTimeKind.Utc).AddTicks(6870), "$2a$11$3HO2VtpvbAzX1nVdQcKS0.ObKHCbdqT3EaJrO.zEezdrdraJy1Qb.", new DateTime(2025, 12, 20, 8, 34, 14, 665, DateTimeKind.Utc).AddTicks(6874) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 784, DateTimeKind.Utc).AddTicks(7949), "$2a$11$w2A/A2JoyhECKb/wPsW5vOBGGNPN0SrOxCb.huffVCaPLgOpuj9s6", new DateTime(2025, 12, 20, 8, 34, 14, 784, DateTimeKind.Utc).AddTicks(8030) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 901, DateTimeKind.Utc).AddTicks(3806), "$2a$11$hXPdC8E8.U8Pgz5qUxwM1.IWEt37c8j3YmXS5bGuEOLXOWyY4J5YC", new DateTime(2025, 12, 20, 8, 34, 14, 901, DateTimeKind.Utc).AddTicks(3809) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 190, DateTimeKind.Utc).AddTicks(1670), "$2a$11$VMBAZHm0Rzd7coz6pmj2UOu3aHZk5o9Vb2odgA9FlbiQ./f8Vd/FK", new DateTime(2025, 12, 20, 8, 34, 14, 190, DateTimeKind.Utc).AddTicks(1693) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 20, 8, 34, 14, 305, DateTimeKind.Utc).AddTicks(2410), "$2a$11$XyDCyzes9ZghVuY1wKl/d.1radJnQhyEuTU8KO9hS65quQBTyn5ty", new DateTime(2025, 12, 20, 8, 34, 14, 305, DateTimeKind.Utc).AddTicks(2415) });
        }
    }
}
