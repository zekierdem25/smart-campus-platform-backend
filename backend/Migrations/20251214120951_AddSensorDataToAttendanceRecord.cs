using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorDataToAttendanceRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SensorAccelerationX",
                table: "AttendanceRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SensorAccelerationY",
                table: "AttendanceRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SensorAccelerationZ",
                table: "AttendanceRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SensorDataUnavailable",
                table: "AttendanceRecords",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorAccelerationX",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "SensorAccelerationY",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "SensorAccelerationZ",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "SensorDataUnavailable",
                table: "AttendanceRecords");

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4166), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4166) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4166), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4166) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000011-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000012-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000013-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000014-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000015-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000016-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000017-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4384), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4384) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000018-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4384), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4384) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000021-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000022-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000023-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000024-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000025-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000026-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000027-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4439), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4439) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000028-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4439), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4439) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000031-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000032-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000033-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000034-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000035-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000036-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000037-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4518), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4518) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000038-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4518), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4518) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000041-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000042-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000043-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000044-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000045-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000046-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000047-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4552), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4552) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000048-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4552), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4552) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000051-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000052-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000053-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000054-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000055-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000056-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000057-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4598), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4598) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000058-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4598), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4598) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000061-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000062-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000063-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000064-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000065-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000066-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000067-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4631), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4631) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000068-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4631), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4631) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000071-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000072-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000073-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000074-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000075-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000076-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000077-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4662), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4662) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000078-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4662), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4662) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000081-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000082-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000083-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000084-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000085-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000086-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000087-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4718), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4718) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000088-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4718), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4718) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000091-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000092-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000093-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000094-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000095-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000096-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9587) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000097-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4749), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4749) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000098-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4749), new DateTime(2025, 12, 14, 11, 45, 28, 351, DateTimeKind.Utc).AddTicks(4749) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3819), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3897) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3972), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3986), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3986) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3989), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3989) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3996), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8271), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8346) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8419), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8419) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8425), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8425) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8429), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8441) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8444), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8445) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8448), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(8448) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5914) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5989), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5989) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5992), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5992) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5995), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5995) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5998), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(5998) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(6015), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(6015) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(6022), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(6022) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 356, DateTimeKind.Utc).AddTicks(4728), new DateTime(2025, 12, 14, 11, 45, 27, 356, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 356, DateTimeKind.Utc).AddTicks(4885), new DateTime(2025, 12, 14, 11, 45, 27, 356, DateTimeKind.Utc).AddTicks(4886) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 356, DateTimeKind.Utc).AddTicks(4888), new DateTime(2025, 12, 14, 11, 45, 27, 356, DateTimeKind.Utc).AddTicks(4889) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9291), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9170), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9361) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9436), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9434), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9437) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9440), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9440), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9441) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9444), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9443), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9444) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9447), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9447), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9447) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9450), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9450), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9451) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9454), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9453), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9454) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9468), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9467), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9468) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9471), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9471), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9472) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9477), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9476), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9477) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 11, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 12, 14, 11, 45, 28, 349, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 773, DateTimeKind.Utc).AddTicks(7993), new DateTime(2025, 12, 14, 11, 45, 27, 773, DateTimeKind.Utc).AddTicks(8078) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 773, DateTimeKind.Utc).AddTicks(8152), new DateTime(2025, 12, 14, 11, 45, 27, 773, DateTimeKind.Utc).AddTicks(8152) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(5874), new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(5959) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6084), new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6085) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6091), new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6091) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6095), new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6095) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6108), new DateTime(2025, 12, 14, 11, 45, 28, 347, DateTimeKind.Utc).AddTicks(6108) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 544, DateTimeKind.Utc).AddTicks(9912), "$2a$11$87b5DnGiPKb4JoptNHzyDebruAlfvuWyYOv9cfQ63yrh6e00uYXeq", new DateTime(2025, 12, 14, 11, 45, 27, 545, DateTimeKind.Utc).AddTicks(56) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 887, DateTimeKind.Utc).AddTicks(7979), "$2a$11$JEkrQtUN5b/zyeV85OgPbecsoeja.93/620WQxksjhYh98SF10WJm", new DateTime(2025, 12, 14, 11, 45, 27, 887, DateTimeKind.Utc).AddTicks(7984) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 2, DateTimeKind.Utc).AddTicks(7419), "$2a$11$LZZ6LdhIphkcIMg6MLzeweNhswmlvvFjtkrixb1aGnsBNXTSDhMoS", new DateTime(2025, 12, 14, 11, 45, 28, 2, DateTimeKind.Utc).AddTicks(7424) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 117, DateTimeKind.Utc).AddTicks(7668), "$2a$11$.1DpXHmAPLUM8nmADRcETu3IdgLeEJt05Nqoafb/F0tXELV442NCa", new DateTime(2025, 12, 14, 11, 45, 28, 117, DateTimeKind.Utc).AddTicks(7738) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 232, DateTimeKind.Utc).AddTicks(7105), "$2a$11$ZZh/FMLorOjk/fDtjiX3aeUkQpf6u9ZznkcmqieGavgTq7kp6.PWa", new DateTime(2025, 12, 14, 11, 45, 28, 232, DateTimeKind.Utc).AddTicks(7107) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 28, 346, DateTimeKind.Utc).AddTicks(2951), "$2a$11$Bpk41WXqUgk3JO78LXCWS.sjD7JAMDfkBOyy3Hu/zGwhyVnVaDoQe", new DateTime(2025, 12, 14, 11, 45, 28, 346, DateTimeKind.Utc).AddTicks(2955) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 659, DateTimeKind.Utc).AddTicks(7280), "$2a$11$HDCg5qzgX/lDoeK45bAEZ.EeHxDCT9xYi6QGG6PMTRrCj/0AJPGjq", new DateTime(2025, 12, 14, 11, 45, 27, 659, DateTimeKind.Utc).AddTicks(7286) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 45, 27, 773, DateTimeKind.Utc).AddTicks(4947), "$2a$11$dcQE9ghyGnGgUfKVauPhueRYwXtM.B75fZz818J6Yn2BeE4UCAbiC", new DateTime(2025, 12, 14, 11, 45, 27, 773, DateTimeKind.Utc).AddTicks(4951) });
        }
    }
}
