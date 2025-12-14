using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHolidaysTo10Years : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"));

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "Description", "EndDate", "StartDate", "Title", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "Description", "EndDate", "StartDate", "Title", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) });

            migrationBuilder.InsertData(
                table: "AcademicEvents",
                columns: new[] { "Id", "CreatedAt", "Description", "EndDate", "StartDate", "Title", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ae000011-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000012-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000013-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000014-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000015-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000016-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2026, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000021-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000022-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2027, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000023-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2027, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000024-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2027, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000025-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2027, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000026-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2027, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000031-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2028, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2028, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000032-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2028, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2028, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000033-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2028, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2028, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000034-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2028, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2028, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000035-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2028, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2028, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000036-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2028, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2028, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000041-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2029, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2029, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000042-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2029, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2029, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000043-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2029, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2029, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000044-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2029, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2029, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000045-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2029, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2029, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000046-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2029, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2029, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000051-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000052-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2030, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000053-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2030, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000054-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2030, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000055-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2030, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000056-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2030, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000061-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2031, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2031, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000062-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2031, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2031, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000063-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2031, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2031, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000064-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2031, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2031, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000065-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2031, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2031, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000066-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2031, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2031, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000071-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2032, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2032, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000072-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2032, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2032, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000073-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2032, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2032, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000074-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2032, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2032, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000075-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2032, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2032, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000076-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2032, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2032, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000081-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2033, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2033, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000082-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2033, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2033, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000083-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2033, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2033, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000084-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2033, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2033, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000085-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2033, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2033, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000086-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2033, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2033, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000091-0001-0001-0001-000000000001"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "Yeni Yıl Tatili", new DateTime(2034, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2034, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeni Yıl", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000092-0002-0002-0002-000000000002"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "23 Nisan Ulusal Egemenlik ve Çocuk Bayramı", new DateTime(2034, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2034, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ulusal Egemenlik ve Çocuk Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000093-0003-0003-0003-000000000003"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "1 Mayıs Emek ve Dayanışma Günü", new DateTime(2034, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2034, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emek ve Dayanışma Günü", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000094-0004-0004-0004-000000000004"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "19 Mayıs Atatürk'ü Anma, Gençlik ve Spor Bayramı", new DateTime(2034, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2034, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atatürk'ü Anma, Gençlik ve Spor Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000095-0005-0005-0005-000000000005"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "30 Ağustos Zafer Bayramı", new DateTime(2034, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2034, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) },
                    { new Guid("ae000096-0006-0006-0006-000000000006"), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657), "29 Ekim Cumhuriyet Bayramı", new DateTime(2034, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2034, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumhuriyet Bayramı", "Holiday", new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1657) }
                });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2762), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2933), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2933) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2936), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2937) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2948), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2949) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2952), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(2952) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(8782), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(8973) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9169), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9169) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9174), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9175) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9189), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9189) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9193), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9193) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9201), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(9201) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5251) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5332), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5332) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5336), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5336) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5352), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5352) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5355), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5355) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5358), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5358) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5365), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5366) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5368), new DateTime(2025, 12, 14, 11, 42, 3, 449, DateTimeKind.Utc).AddTicks(5369) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 457, DateTimeKind.Utc).AddTicks(4603), new DateTime(2025, 12, 14, 11, 42, 2, 457, DateTimeKind.Utc).AddTicks(4688) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 457, DateTimeKind.Utc).AddTicks(4767), new DateTime(2025, 12, 14, 11, 42, 2, 457, DateTimeKind.Utc).AddTicks(4768) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 457, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 12, 14, 11, 42, 2, 457, DateTimeKind.Utc).AddTicks(4771) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(955), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1226) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1316), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1313), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1316) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1320), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1319), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1320) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1340), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1323), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1340) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1344), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1343), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1344) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1347), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1347), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1347) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1351), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1350), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1351) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1358), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1358), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1359) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1362), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1361), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1362) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1366), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1365), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1366) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1369), new DateTime(2025, 11, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1368), new DateTime(2025, 12, 14, 11, 42, 3, 450, DateTimeKind.Utc).AddTicks(1369) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 871, DateTimeKind.Utc).AddTicks(7616), new DateTime(2025, 12, 14, 11, 42, 2, 871, DateTimeKind.Utc).AddTicks(7701) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 871, DateTimeKind.Utc).AddTicks(7781), new DateTime(2025, 12, 14, 11, 42, 2, 871, DateTimeKind.Utc).AddTicks(7781) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8331), new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8438) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8604), new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8604) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8624), new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8624) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8634), new DateTime(2025, 12, 14, 11, 42, 3, 446, DateTimeKind.Utc).AddTicks(8634) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 636, DateTimeKind.Utc).AddTicks(8148), "$2a$11$ZiOKkkWHpfnh5MOLOytHhe2rqYscpZcsD1NpgS0wNlkF4QX33JyPO", new DateTime(2025, 12, 14, 11, 42, 2, 636, DateTimeKind.Utc).AddTicks(8299) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 987, DateTimeKind.Utc).AddTicks(7089), "$2a$11$W0OE71PQQC4eZUIeflacCeLpGSYBA.nKnHCswjiwkLH4q256Fvk46", new DateTime(2025, 12, 14, 11, 42, 2, 987, DateTimeKind.Utc).AddTicks(7094) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 102, DateTimeKind.Utc).AddTicks(2510), "$2a$11$MU1sBpiwZS/z/KDFk221AuDruKOoVCHDQw8C8BYuOJC4Xyb5gQAJm", new DateTime(2025, 12, 14, 11, 42, 3, 102, DateTimeKind.Utc).AddTicks(2515) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 215, DateTimeKind.Utc).AddTicks(9173), "$2a$11$7nn9wtiYqwwLhQOaMAfwAeQes6J5fx4lVfyjYBJNxOffBUkqhlY/e", new DateTime(2025, 12, 14, 11, 42, 3, 215, DateTimeKind.Utc).AddTicks(9178) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 330, DateTimeKind.Utc).AddTicks(1656), "$2a$11$8kTLzewL06cgUpNGgqzsneclADMXNaWr/XoP4Unb1R1tY1nFdH8mC", new DateTime(2025, 12, 14, 11, 42, 3, 330, DateTimeKind.Utc).AddTicks(1731) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 3, 445, DateTimeKind.Utc).AddTicks(3186), "$2a$11$vSk0TZA68C2bkRl06FbmB./UHHwFu48sfzvvoUQnTunKRO74k9WRS", new DateTime(2025, 12, 14, 11, 42, 3, 445, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 757, DateTimeKind.Utc).AddTicks(4545), "$2a$11$4kMdkvqS06H9QVSg12/WZuxcKXKzF7KCCxbH03xjp4VWVpf5Y/mLm", new DateTime(2025, 12, 14, 11, 42, 2, 757, DateTimeKind.Utc).AddTicks(4550) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 42, 2, 871, DateTimeKind.Utc).AddTicks(3845), "$2a$11$1b8y941qdxcS0dpyykNQY.Y.QsQmPBVESf9DTmkMskmYy0nDyUH9a", new DateTime(2025, 12, 14, 11, 42, 2, 871, DateTimeKind.Utc).AddTicks(3850) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000011-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000012-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000013-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000014-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000015-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000016-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000021-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000022-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000023-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000024-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000025-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000026-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000031-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000032-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000033-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000034-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000035-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000036-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000041-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000042-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000043-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000044-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000045-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000046-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000051-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000052-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000053-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000054-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000055-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000056-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000061-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000062-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000063-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000064-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000065-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000066-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000071-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000072-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000073-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000074-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000075-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000076-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000081-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000082-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000083-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000084-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000085-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000086-0006-0006-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000091-0001-0001-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000092-0002-0002-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000093-0003-0003-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000094-0004-0004-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000095-0005-0005-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000096-0006-0006-0006-000000000006"));

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2181), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2261) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2348), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2349) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2352), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2353) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2355), new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2356) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "Description", "EndDate", "StartDate", "Title", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2358), "Ramazan Bayramı (3 gün)", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ramazan Bayramı", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2359) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "Description", "EndDate", "StartDate", "Title", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2362), "30 Ağustos Zafer Bayramı", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zafer Bayramı", new DateTime(2025, 12, 14, 11, 35, 13, 420, DateTimeKind.Utc).AddTicks(2362) });

            migrationBuilder.InsertData(
                table: "AcademicEvents",
                columns: new[] { "Id", "CreatedAt", "Description", "EndDate", "StartDate", "Title", "Type", "UpdatedAt" },
                values: new object[,]
                {
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
    }
}
