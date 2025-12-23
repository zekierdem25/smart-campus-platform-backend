using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Category = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PushEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SmsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RelatedEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RelatedEntityType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
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
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6153), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6153) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6153), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6153) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6242), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6242) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6242), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6242) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6272), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6272) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6272), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6272) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6310), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6310), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6328), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6328) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6328), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6328) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6346), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6346) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6346), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6346) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6363), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6363) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6363), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6363) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6387), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6387) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6387), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6387) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(86), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(174) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(260), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(261) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3181) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3255), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3255) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3258), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3259) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3269), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3269) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3272), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(3272) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7345), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7417) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7491), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7492) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7495), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7496) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7504), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7505) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7523), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7523) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7527), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(7527) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5085) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5168), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5169) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5172), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5172) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5175), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5175) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5178), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5178) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5181) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5187), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5187) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 322, DateTimeKind.Utc).AddTicks(2708), new DateTime(2025, 12, 23, 21, 18, 6, 322, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 322, DateTimeKind.Utc).AddTicks(2882), new DateTime(2025, 12, 23, 21, 18, 6, 322, DateTimeKind.Utc).AddTicks(2882) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 322, DateTimeKind.Utc).AddTicks(2885), new DateTime(2025, 12, 23, 21, 18, 6, 322, DateTimeKind.Utc).AddTicks(2885) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8490), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8350), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8559) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8628), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8631) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8645), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8645), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8646) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8649), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8648), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8649) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8652), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8652), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8653) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8655), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8655), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8656) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8659), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8658), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8659) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8662), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8661), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8662) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8667), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8666), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8667) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8670), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8670), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8670) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8673), new DateTime(2025, 11, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8673), new DateTime(2025, 12, 23, 21, 18, 7, 370, DateTimeKind.Utc).AddTicks(8674) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3163), new DateTime(2026, 1, 17, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(2668), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3266) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3370), new DateTime(2025, 12, 27, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3369), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3371) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3794), new DateTime(2025, 12, 31, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3375), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3795) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e44444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3803), new DateTime(2025, 12, 29, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3802), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3803) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 765, DateTimeKind.Utc).AddTicks(2112), new DateTime(2025, 12, 23, 21, 18, 6, 765, DateTimeKind.Utc).AddTicks(2215) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 765, DateTimeKind.Utc).AddTicks(2305), new DateTime(2025, 12, 23, 21, 18, 6, 765, DateTimeKind.Utc).AddTicks(2306) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00001-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6045), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00002-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6340), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6341) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00003-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6345), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6346) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00011-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00012-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6360), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00013-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6364), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6364) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00021-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6368), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6368) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00022-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6371), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6372) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00023-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6375), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6375) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00031-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6379), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6379) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00032-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6382), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6383) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00033-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6386), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6386) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00041-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6392), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6392) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00042-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6395), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6395) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00043-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6399), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(6399) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8551), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8694), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8694) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8698), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8698) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8702), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8702) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8705), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8705) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8711), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8712) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8715), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8716) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8719), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8719) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8723), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8723) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8726), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8727) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 12, 23, 21, 18, 7, 371, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1377), new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1589), new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1604), new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1604) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1609), new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1614), new DateTime(2025, 12, 23, 21, 18, 7, 368, DateTimeKind.Utc).AddTicks(1614) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 512, DateTimeKind.Utc).AddTicks(5256), "$2a$11$S5E8mohAwO1C0QCtw7/PQOsEQ7j8l0/WlU65E2uWzQYtFEfZxTh7u", new DateTime(2025, 12, 23, 21, 18, 6, 512, DateTimeKind.Utc).AddTicks(5379) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 894, DateTimeKind.Utc).AddTicks(6385), "$2a$11$.7jXCHVVfMVFnrNt1akexeYIUC7IYea8MXXhNznAo7CCOr75oEXX.", new DateTime(2025, 12, 23, 21, 18, 6, 894, DateTimeKind.Utc).AddTicks(6392) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 13, DateTimeKind.Utc).AddTicks(3533), "$2a$11$HlCtTD2UsRgEOJNLZYYBpewy.6sgYqSkKyDcXnkbWY2QcN8lYzI66", new DateTime(2025, 12, 23, 21, 18, 7, 13, DateTimeKind.Utc).AddTicks(3538) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 130, DateTimeKind.Utc).AddTicks(5297), "$2a$11$AICcppeYK48rgrdxjZOX.uaXyrKy7W0yu4upg8FD/yxaMiRbo3U0u", new DateTime(2025, 12, 23, 21, 18, 7, 130, DateTimeKind.Utc).AddTicks(5306) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 246, DateTimeKind.Utc).AddTicks(7148), "$2a$11$3Rkwv0FmA3NOvCKr.vFQjOFHv9LVorCcsEVL.aq2CFdG7cr6VFByG", new DateTime(2025, 12, 23, 21, 18, 7, 246, DateTimeKind.Utc).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 366, DateTimeKind.Utc).AddTicks(7823), "$2a$11$I337UVuil9uc7UVV99.Tred4CexLvJ2zZGJzus2r0JwUl72M0bv5C", new DateTime(2025, 12, 23, 21, 18, 7, 366, DateTimeKind.Utc).AddTicks(7828) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 641, DateTimeKind.Utc).AddTicks(2821), "$2a$11$EifnYtDLF6ueUa6YWZV7wObhkz6HceaHr8HvhaF1Vse4nX7/S3oN2", new DateTime(2025, 12, 23, 21, 18, 6, 641, DateTimeKind.Utc).AddTicks(2826) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 6, 764, DateTimeKind.Utc).AddTicks(8194), "$2a$11$iLeVebNyas7ucAM9fb8h8OI28xvzZUVehpNRpitPzTnSNEpVVWFKG", new DateTime(2025, 12, 23, 21, 18, 6, 764, DateTimeKind.Utc).AddTicks(8203) });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPreferences_UserId_Category",
                table: "NotificationPreferences",
                columns: new[] { "UserId", "Category" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Category",
                table: "Notifications",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedAt",
                table: "Notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationPreferences");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8110), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8110) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8110), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8110) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8183), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8183) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8183), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8183) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8215), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8215) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8215), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8215) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8239), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8239) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8239), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8239) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8256), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8256) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8256), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8256) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8282), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8282) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8282), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8282) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8306), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8306) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8306), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8306) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8324), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8324) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8324), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8324) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8464), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8464) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8464), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8464) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(947) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8488), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8488) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8488), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(8488) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(811), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(896) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(977), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(977) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5307) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5388), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5389) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5399), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5402), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5403) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5405), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(5405) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9545), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9621) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9693), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9694) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9697), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9698) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9721), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9721) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9724), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9725) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9730), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(9731) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7226), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7304) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7377), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7378) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7381), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7381) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7384), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7384) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7386), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7387) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7406), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7406) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7409), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7412), new DateTime(2025, 12, 23, 14, 23, 57, 884, DateTimeKind.Utc).AddTicks(7412) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 56, 802, DateTimeKind.Utc).AddTicks(9469), new DateTime(2025, 12, 23, 14, 23, 56, 802, DateTimeKind.Utc).AddTicks(9567) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 56, 802, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 12, 23, 14, 23, 56, 802, DateTimeKind.Utc).AddTicks(9653) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 56, 802, DateTimeKind.Utc).AddTicks(9656), new DateTime(2025, 12, 23, 14, 23, 56, 802, DateTimeKind.Utc).AddTicks(9657) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(635), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(482), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(702) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(774), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(772), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(775) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(779), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(778), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(779) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(782), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(782), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(782) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(785), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(785), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(786) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(789), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(788), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(789) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(805), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(804), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(805) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(811), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(810), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(811) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(814), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(814), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(815) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(818), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(817), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(818) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(822), new DateTime(2025, 11, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(821), new DateTime(2025, 12, 23, 14, 23, 57, 885, DateTimeKind.Utc).AddTicks(822) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 1, 17, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(2427), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(2924) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3025), new DateTime(2025, 12, 27, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3010), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3026) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3328), new DateTime(2025, 12, 31, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3031), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3328) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e44444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3336), new DateTime(2025, 12, 29, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3335), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(3337) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 289, DateTimeKind.Utc).AddTicks(4611), new DateTime(2025, 12, 23, 14, 23, 57, 289, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 289, DateTimeKind.Utc).AddTicks(4783), new DateTime(2025, 12, 23, 14, 23, 57, 289, DateTimeKind.Utc).AddTicks(4784) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00001-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4899) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00002-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4975), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4976) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00003-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4981) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00011-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4988), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4988) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00012-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4993), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(4993) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00013-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00021-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5013), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5013) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00022-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5017), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5018) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00023-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5021), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5021) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00031-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5024), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5024) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00032-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5027), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5028) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00033-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5033), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5033) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00041-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5036), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5036) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00042-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5039), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00043-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5043), new DateTime(2025, 12, 23, 14, 23, 57, 887, DateTimeKind.Utc).AddTicks(5043) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(566), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(634) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(708), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(708) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(712), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(712) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(715), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(715) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(722), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(722) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(725), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(726) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(729), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(729) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(732), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(732) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(735), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(736) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(739), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(739) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(742), new DateTime(2025, 12, 23, 14, 23, 57, 886, DateTimeKind.Utc).AddTicks(742) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(2933), new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3020) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3133), new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3134) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3145), new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3145) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3149), new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3149) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3153), new DateTime(2025, 12, 23, 14, 23, 57, 882, DateTimeKind.Utc).AddTicks(3153) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 10, DateTimeKind.Utc).AddTicks(7649), "$2a$11$P8igZ9MPfjkYzwdFeZmobuVYce.c5M5MuhJme4tchANodlBslnfvu", new DateTime(2025, 12, 23, 14, 23, 57, 10, DateTimeKind.Utc).AddTicks(7818) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 407, DateTimeKind.Utc).AddTicks(8029), "$2a$11$k433C0QcCNnWU4jw52PyNu89lqUaIw.GUsS.MzVeKXOcmpq1A.kO2", new DateTime(2025, 12, 23, 14, 23, 57, 407, DateTimeKind.Utc).AddTicks(8034) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 525, DateTimeKind.Utc).AddTicks(2608), "$2a$11$KYi1DggZoQiH/.rsYs285OjLkocxUQ4n7ZfPz9RUi0PkML4sW/Vk.", new DateTime(2025, 12, 23, 14, 23, 57, 525, DateTimeKind.Utc).AddTicks(2612) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 650, DateTimeKind.Utc).AddTicks(84), "$2a$11$B0GNg9zVwWYiOMNJxtW.LuJdrwiz9PalTM0OKWHzV7jXu4BeYrage", new DateTime(2025, 12, 23, 14, 23, 57, 650, DateTimeKind.Utc).AddTicks(90) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 766, DateTimeKind.Utc).AddTicks(8582), "$2a$11$56DgpCXTVZnHGEO20g4vz.SJckecf4vCvglXF2iAyTM4jGbLWfnOW", new DateTime(2025, 12, 23, 14, 23, 57, 766, DateTimeKind.Utc).AddTicks(8647) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 880, DateTimeKind.Utc).AddTicks(8561), "$2a$11$J1rNroY/xJwZrJbOkz4SSuLc1bY9YX5vXrM.a9WuxFdvX8jClbpAe", new DateTime(2025, 12, 23, 14, 23, 57, 880, DateTimeKind.Utc).AddTicks(8565) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 165, DateTimeKind.Utc).AddTicks(7128), "$2a$11$pM1EouXZuO5jYKh7oAeIuOd9n8uFxcK7X22oTJrKB3V.Pkaax8Xje", new DateTime(2025, 12, 23, 14, 23, 57, 165, DateTimeKind.Utc).AddTicks(7133) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 14, 23, 57, 289, DateTimeKind.Utc).AddTicks(759), "$2a$11$Bh5WGo2x8KXzCtnFrkqbHuDMUtKvEOdhUOvdXtvFbvZwsJR/NfJCS", new DateTime(2025, 12, 23, 14, 23, 57, 289, DateTimeKind.Utc).AddTicks(807) });
        }
    }
}
