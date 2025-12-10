using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class SorunCozucu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 47, 687, DateTimeKind.Utc).AddTicks(8735), new DateTime(2025, 12, 10, 12, 56, 47, 687, DateTimeKind.Utc).AddTicks(8820) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 47, 687, DateTimeKind.Utc).AddTicks(8893), new DateTime(2025, 12, 10, 12, 56, 47, 687, DateTimeKind.Utc).AddTicks(8894) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 47, 687, DateTimeKind.Utc).AddTicks(8896), new DateTime(2025, 12, 10, 12, 56, 47, 687, DateTimeKind.Utc).AddTicks(8897) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 85, DateTimeKind.Utc).AddTicks(7705), new DateTime(2025, 12, 10, 12, 56, 48, 85, DateTimeKind.Utc).AddTicks(7795) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 85, DateTimeKind.Utc).AddTicks(7883), new DateTime(2025, 12, 10, 12, 56, 48, 85, DateTimeKind.Utc).AddTicks(7883) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 650, DateTimeKind.Utc).AddTicks(9845), new DateTime(2025, 12, 10, 12, 56, 48, 650, DateTimeKind.Utc).AddTicks(9931) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(38), new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(39) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(49), new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(49) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(53), new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(53) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(61), new DateTime(2025, 12, 10, 12, 56, 48, 651, DateTimeKind.Utc).AddTicks(61) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 47, 859, DateTimeKind.Utc).AddTicks(7299), "$2a$11$bComwpVKmO5bYfrkshNhnur9pd4OyobtXLiHmm5rzV1eAUGcAqBlK", new DateTime(2025, 12, 10, 12, 56, 47, 859, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 199, DateTimeKind.Utc).AddTicks(1657), "$2a$11$GRcyJyohvFiwcwjvIVCzQObjpfLEgbDVJlFULx/DMLs5Wb//Ov1xi", new DateTime(2025, 12, 10, 12, 56, 48, 199, DateTimeKind.Utc).AddTicks(1661) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 312, DateTimeKind.Utc).AddTicks(3187), "$2a$11$bpBW6RW4rN4IhaFSDvvItuh.1TH5U4xi2/6hHIF13l92sTJRUc5xe", new DateTime(2025, 12, 10, 12, 56, 48, 312, DateTimeKind.Utc).AddTicks(3191) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 424, DateTimeKind.Utc).AddTicks(6747), "$2a$11$O0/OmHEllYEDM3tmjv6dDeJTeCFrRDWcUlv9cyiUCK1eTFG/EpUtu", new DateTime(2025, 12, 10, 12, 56, 48, 424, DateTimeKind.Utc).AddTicks(6818) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 537, DateTimeKind.Utc).AddTicks(6513), "$2a$11$c97760inAV5cmt.E0MFld.nwWM34vn.RQxXZwWtDjab/Lm3g4Zxq6", new DateTime(2025, 12, 10, 12, 56, 48, 537, DateTimeKind.Utc).AddTicks(6516) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 649, DateTimeKind.Utc).AddTicks(7761), "$2a$11$WrvQk7c7fURTOJg/IME3IusCI.TD3BEyNlnoAwcdfPMZ.uXPtZb8W", new DateTime(2025, 12, 10, 12, 56, 48, 649, DateTimeKind.Utc).AddTicks(7763) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 47, 972, DateTimeKind.Utc).AddTicks(1721), "$2a$11$/MSzYTfaauJGG2MqhqrUxOKjBYEimUNrIKo2mX9RGfAuChBgAF1My", new DateTime(2025, 12, 10, 12, 56, 47, 972, DateTimeKind.Utc).AddTicks(1726) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 56, 48, 85, DateTimeKind.Utc).AddTicks(4747), "$2a$11$kCoMKJacXGCp73qjtHguKeg4YXk274Nc.JlKbo9SQ5tsvij5UvWGi", new DateTime(2025, 12, 10, 12, 56, 48, 85, DateTimeKind.Utc).AddTicks(4752) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 21, 766, DateTimeKind.Utc).AddTicks(3706), new DateTime(2025, 12, 10, 12, 34, 21, 766, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 21, 766, DateTimeKind.Utc).AddTicks(3867), new DateTime(2025, 12, 10, 12, 34, 21, 766, DateTimeKind.Utc).AddTicks(3867) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 21, 766, DateTimeKind.Utc).AddTicks(3869), new DateTime(2025, 12, 10, 12, 34, 21, 766, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 175, DateTimeKind.Utc).AddTicks(2424), new DateTime(2025, 12, 10, 12, 34, 22, 175, DateTimeKind.Utc).AddTicks(2513) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 175, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 12, 10, 12, 34, 22, 175, DateTimeKind.Utc).AddTicks(2591) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(1852), new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(1943) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2068), new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2068) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2073), new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2074) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2084), new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2085) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2089), new DateTime(2025, 12, 10, 12, 34, 22, 741, DateTimeKind.Utc).AddTicks(2089) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 21, 940, DateTimeKind.Utc).AddTicks(9411), "$2a$11$eAfcBVTTkFCGTX12Q.zYDO7HVhIKZ4N3Qu9cISQ4/QOK80KXgqnC.", new DateTime(2025, 12, 10, 12, 34, 21, 940, DateTimeKind.Utc).AddTicks(9609) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 287, DateTimeKind.Utc).AddTicks(5347), "$2a$11$bm13L6Y3auKCbKkZ4IuaW.93gqBQkoLaJRytQAN/yINcScYn34Qey", new DateTime(2025, 12, 10, 12, 34, 22, 287, DateTimeKind.Utc).AddTicks(5353) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 399, DateTimeKind.Utc).AddTicks(8913), "$2a$11$qzbW3lZcFD6SuEzXu8ALV.BBmqD1ho93Z/v7.zPztu703DqSxS9E.", new DateTime(2025, 12, 10, 12, 34, 22, 399, DateTimeKind.Utc).AddTicks(8918) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 512, DateTimeKind.Utc).AddTicks(1562), "$2a$11$WmTOwllh5m3AEf7Zmibinu4T6seefe.5pqCfUFVraY0B3JTzkS0s6", new DateTime(2025, 12, 10, 12, 34, 22, 512, DateTimeKind.Utc).AddTicks(1567) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 624, DateTimeKind.Utc).AddTicks(6137), "$2a$11$AbviA5TuZ6qz2F7g3qq5k.9xnqJRgEMKsBmnTtVDCVWNmxDIgV.ie", new DateTime(2025, 12, 10, 12, 34, 22, 624, DateTimeKind.Utc).AddTicks(6213) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 739, DateTimeKind.Utc).AddTicks(9698), "$2a$11$u162/vOLXHE2xT8AuyDaGeU0MtXLIJDoiOt3Y.asZV91BvSvPjnji", new DateTime(2025, 12, 10, 12, 34, 22, 739, DateTimeKind.Utc).AddTicks(9703) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 62, DateTimeKind.Utc).AddTicks(8559), "$2a$11$gM61x9NJS3Q1BR39wRQ2IOJeiApFTnPHrWW6QWgKD5sjGdXeyaadW", new DateTime(2025, 12, 10, 12, 34, 22, 62, DateTimeKind.Utc).AddTicks(8565) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 34, 22, 174, DateTimeKind.Utc).AddTicks(9436), "$2a$11$X40S7yhh6Y8/DVMWLMpKheJs/pSRz2okEpzmfkvcrGcp33AUIKvK2", new DateTime(2025, 12, 10, 12, 34, 22, 174, DateTimeKind.Utc).AddTicks(9438) });
        }
    }
}
