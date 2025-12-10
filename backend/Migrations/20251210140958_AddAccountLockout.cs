using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountLockout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedLoginAttempts",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockoutEndAt",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

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
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 63, DateTimeKind.Utc).AddTicks(1617), 0, null, "$2a$11$woFIq/nSuIJ1dYfnDufiCucXwo.ToSF2vpBpxBJWNRT9e9DcTedQG", new DateTime(2025, 12, 10, 14, 9, 57, 63, DateTimeKind.Utc).AddTicks(1934) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 409, DateTimeKind.Utc).AddTicks(2865), 0, null, "$2a$11$MT0NmRu/wWSYslsFuz6.feJf90.HpoenGX5tZv83CiYhS1Eo4BN7q", new DateTime(2025, 12, 10, 14, 9, 57, 409, DateTimeKind.Utc).AddTicks(2869) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 523, DateTimeKind.Utc).AddTicks(6899), 0, null, "$2a$11$qXIkjZtb.R3uUMiIIc.1fOTQCF45hFnU9vNnETcPA0JAifYitkkH6", new DateTime(2025, 12, 10, 14, 9, 57, 523, DateTimeKind.Utc).AddTicks(6904) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 637, DateTimeKind.Utc).AddTicks(4765), 0, null, "$2a$11$o5KW4kFNCC3XIRAtyz42SeMkEuhQiIs9FwRvzrAXg35CzK06/.JXG", new DateTime(2025, 12, 10, 14, 9, 57, 637, DateTimeKind.Utc).AddTicks(4832) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 751, DateTimeKind.Utc).AddTicks(3849), 0, null, "$2a$11$pTSdtF2u3tcSwyCQhpyQ5uyJgYWnb6w9qc7pUr7Ry8dZtI8xHP9Ry", new DateTime(2025, 12, 10, 14, 9, 57, 751, DateTimeKind.Utc).AddTicks(3852) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 865, DateTimeKind.Utc).AddTicks(987), 0, null, "$2a$11$U/b4s4nRXieLT6URFbA/Xuslx.d86inqqNB0967QofzSVVTHj8KjS", new DateTime(2025, 12, 10, 14, 9, 57, 865, DateTimeKind.Utc).AddTicks(990) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 180, DateTimeKind.Utc).AddTicks(8720), 0, null, "$2a$11$xuDSEPXho1DbcmX6fSjMy.jJdERxXSPj2p//evBNcWyjH4G8pBf5C", new DateTime(2025, 12, 10, 14, 9, 57, 180, DateTimeKind.Utc).AddTicks(8725) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "FailedLoginAttempts", "LockoutEndAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(314), 0, null, "$2a$11$9Tq9jVGlAKAUTbQqCo2/meXzDXjrr6ajMNWMA8HUaOv.CFv5O/61C", new DateTime(2025, 12, 10, 14, 9, 57, 295, DateTimeKind.Utc).AddTicks(318) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedLoginAttempts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEndAt",
                table: "Users");

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
    }
}
