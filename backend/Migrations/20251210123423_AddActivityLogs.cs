using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Action = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IpAddress = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAgent = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId_CreatedAt",
                table: "ActivityLogs",
                columns: new[] { "UserId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 55, 738, DateTimeKind.Utc).AddTicks(6617), new DateTime(2025, 12, 8, 19, 47, 55, 738, DateTimeKind.Utc).AddTicks(6804) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 55, 738, DateTimeKind.Utc).AddTicks(6940), new DateTime(2025, 12, 8, 19, 47, 55, 738, DateTimeKind.Utc).AddTicks(6940) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 55, 738, DateTimeKind.Utc).AddTicks(6944), new DateTime(2025, 12, 8, 19, 47, 55, 738, DateTimeKind.Utc).AddTicks(6945) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 189, DateTimeKind.Utc).AddTicks(4731), new DateTime(2025, 12, 8, 19, 47, 56, 189, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 189, DateTimeKind.Utc).AddTicks(4899), new DateTime(2025, 12, 8, 19, 47, 56, 189, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(607), new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(702) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(826), new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(826) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(831), new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(831) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(839), new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(840) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(844), new DateTime(2025, 12, 8, 19, 47, 56, 793, DateTimeKind.Utc).AddTicks(844) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 55, 943, DateTimeKind.Utc).AddTicks(6834), "$2a$11$EgF3NOoh1JR8gOB0NG6p1uPP3ogCbk969Vy9CqgcirPBELzmoJUhm", new DateTime(2025, 12, 8, 19, 47, 55, 943, DateTimeKind.Utc).AddTicks(6972) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 308, DateTimeKind.Utc).AddTicks(5296), "$2a$11$GA8mcOU1epYWwD5dctcd/eQWE89I3n/092070aWsEKXQpfm0XGnKO", new DateTime(2025, 12, 8, 19, 47, 56, 308, DateTimeKind.Utc).AddTicks(5302) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 430, DateTimeKind.Utc).AddTicks(4343), "$2a$11$wWTBhpIlO8Vsr6I9hPrhz.XcVgUmlKpqDiLfcmLUiGbXHh0Jd/lq6", new DateTime(2025, 12, 8, 19, 47, 56, 430, DateTimeKind.Utc).AddTicks(4347) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 553, DateTimeKind.Utc).AddTicks(1517), "$2a$11$vaGDBjIr3F9v8YM56TXy4u51.svX3fHhU0q/iDjsN1HlJNbp6B/iW", new DateTime(2025, 12, 8, 19, 47, 56, 553, DateTimeKind.Utc).AddTicks(1521) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 672, DateTimeKind.Utc).AddTicks(5017), "$2a$11$zSVth1/5cIxu/m6gDtoa0epm/RY.14wRhkn55wgg4f1lPmZxu6YyO", new DateTime(2025, 12, 8, 19, 47, 56, 672, DateTimeKind.Utc).AddTicks(5107) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 791, DateTimeKind.Utc).AddTicks(4670), "$2a$11$gDpUyBfxqqXkntkcs.r2quMqQNcxmhsvI2QxiV9ldnMwt4rGljolu", new DateTime(2025, 12, 8, 19, 47, 56, 791, DateTimeKind.Utc).AddTicks(4674) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 68, DateTimeKind.Utc).AddTicks(7814), "$2a$11$PUFWmnDXLWrQ/LU8f4tQpe0xpAnHb/nprMfJeRLlfxFPQNA/aH5DO", new DateTime(2025, 12, 8, 19, 47, 56, 68, DateTimeKind.Utc).AddTicks(7818) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 19, 47, 56, 189, DateTimeKind.Utc).AddTicks(2180), "$2a$11$Gnj/A62u44.hRN0/BOvrue15Q788rwlrcXa/vUQW4/dtxHFooM3hi", new DateTime(2025, 12, 8, 19, 47, 56, 189, DateTimeKind.Utc).AddTicks(2184) });
        }
    }
}
