using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPart3SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalorieCount",
                table: "MealMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "MealMenus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.InsertData(
                table: "Cafeterias",
                columns: new[] { "Id", "Capacity", "CreatedAt", "IsActive", "Location", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("caf11111-1111-1111-1111-111111111111"), 500, new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9671), true, "Kampüs Merkezi", "Ana Yemekhane", new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9762) },
                    { new Guid("caf22222-2222-2222-2222-222222222222"), 200, new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9840), true, "Mühendislik Fakültesi", "Mühendislik Kantini", new DateTime(2025, 12, 20, 8, 34, 14, 907, DateTimeKind.Utc).AddTicks(9840) }
                });

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

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Category", "CreatedAt", "CreatedBy", "Date", "Description", "EndTime", "IsPaid", "Location", "Price", "RegisteredCount", "RegistrationDeadline", "StartTime", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("e0e11111-1111-1111-1111-111111111111"), 1000, "Social", new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(1961), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Yıllık geleneksel bahar şenliği etkinlikleri.", new TimeSpan(0, 18, 0, 0, 0), false, "Kampüs Meydanı", null, 0, new DateTime(2026, 1, 14, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(1593), new TimeSpan(0, 10, 0, 0, 0), "Published", "Bahar Şenliği 2024", new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2034) },
                    { new Guid("e0e22222-2222-2222-2222-222222222222"), 150, "Conference", new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2114), new Guid("f1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Geleceğin teknolojisi yapay zeka üzerine derinlemesine bir bakış.", new TimeSpan(0, 16, 0, 0, 0), false, "Konferans Salonu A", null, 0, new DateTime(2025, 12, 24, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2113), new TimeSpan(0, 14, 0, 0, 0), "Published", "Yapay Zeka Semineri", new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2115) },
                    { new Guid("e0e33333-3333-3333-3333-333333333333"), 30, "Workshop", new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2409), new Guid("f1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Başlangıç seviyesi Python programlama atölyesi.", new TimeSpan(0, 12, 0, 0, 0), true, "Bilgisayar Lab 1", 50.00m, 0, new DateTime(2025, 12, 28, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2120), new TimeSpan(0, 9, 0, 0, 0), "Published", "Python Atölyesi", new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(2409) }
                });

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

            migrationBuilder.InsertData(
                table: "MealMenus",
                columns: new[] { "Id", "CafeteriaId", "CalorieCount", "CreatedAt", "Date", "IsPublished", "ItemsJson", "MealType", "NutritionJson", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaa11111-1111-1111-1111-111111111111"), new Guid("caf11111-1111-1111-1111-111111111111"), 850, new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3637), new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), false, "[\"Mercimek Çorbası\", \"Orman Kebabı\", \"Pirinç Pilavı\", \"Ayran\"]", "Lunch", null, 20.00m, new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3709) },
                    { new Guid("aaa22222-2222-2222-2222-222222222222"), new Guid("caf11111-1111-1111-1111-111111111111"), 750, new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3782), new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), false, "[\"Domates Çorbası\", \"Tavuk Sote\", \"Bulgur Pilavı\", \"Meyve\"]", "Dinner", null, 20.00m, new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3782) },
                    { new Guid("aaa33333-3333-3333-3333-333333333333"), new Guid("caf22222-2222-2222-2222-222222222222"), 900, new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3792), new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), false, "[\"Ezogelin Çorbası\", \"İzmir Köfte\", \"Makarna\", \"Salata\"]", "Lunch", null, 25.00m, new DateTime(2025, 12, 20, 8, 34, 14, 908, DateTimeKind.Utc).AddTicks(3793) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e11111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e22222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e33333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa11111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa22222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa33333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf11111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf22222-2222-2222-2222-222222222222"));

            migrationBuilder.DropColumn(
                name: "CalorieCount",
                table: "MealMenus");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MealMenus");

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
        }
    }
}
