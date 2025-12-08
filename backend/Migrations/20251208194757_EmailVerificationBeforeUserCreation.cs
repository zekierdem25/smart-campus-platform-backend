using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Migrations
{
    /// <inheritdoc />
    public partial class EmailVerificationBeforeUserCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "EmailVerificationTokens",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "RegistrationData",
                table: "EmailVerificationTokens",
                type: "TEXT",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationData",
                table: "EmailVerificationTokens");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "EmailVerificationTokens",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4099), new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4292), new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4293) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4295), new DateTime(2025, 12, 5, 17, 18, 54, 852, DateTimeKind.Utc).AddTicks(4296) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9078), new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9169) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9272), new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(9273) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6074), new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6188) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6314), new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6314) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6319), new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6319) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6323), new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6323) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6327), new DateTime(2025, 12, 5, 17, 18, 55, 891, DateTimeKind.Utc).AddTicks(6327) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 44, DateTimeKind.Utc).AddTicks(211), "$2a$11$YGQNYf297HWYE1cBo/IqAOpo67w2iVmpcQw03hZxT0VNYWUu.3Rum", new DateTime(2025, 12, 5, 17, 18, 55, 44, DateTimeKind.Utc).AddTicks(438) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 408, DateTimeKind.Utc).AddTicks(8210), "$2a$11$VR/CI6myzwqMI4rDbxowQeU9fsas/eqfZNVIHNCMPUaZSveyz8z/q", new DateTime(2025, 12, 5, 17, 18, 55, 408, DateTimeKind.Utc).AddTicks(8215) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 529, DateTimeKind.Utc).AddTicks(8861), "$2a$11$YHKwZgwtTrFRcg0cB/G4nuxRhL5QKynG3A2FJ6eI/sJtyRqp.J6/y", new DateTime(2025, 12, 5, 17, 18, 55, 529, DateTimeKind.Utc).AddTicks(8866) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 650, DateTimeKind.Utc).AddTicks(3999), "$2a$11$Dq9HVo27eOuWbb.gvG9Bl.siAdwc2sL65lJURQJJr.ipvqKvLCvwS", new DateTime(2025, 12, 5, 17, 18, 55, 650, DateTimeKind.Utc).AddTicks(4003) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 770, DateTimeKind.Utc).AddTicks(1892), "$2a$11$cN.ELCYptOp4tQMk71nAte4u//RVR1r3IPHGAPGPXfGawdzP2rsTu", new DateTime(2025, 12, 5, 17, 18, 55, 770, DateTimeKind.Utc).AddTicks(1972) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 890, DateTimeKind.Utc).AddTicks(3418), "$2a$11$2/XUiiWCzTp0uiHPYINoSOagrBOmfFSfu6YxoQgRHrLSOJMeD4TSe", new DateTime(2025, 12, 5, 17, 18, 55, 890, DateTimeKind.Utc).AddTicks(3422) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 168, DateTimeKind.Utc).AddTicks(5121), "$2a$11$n1Gg6g3QBeIG5WJ3Bgj/NeJWUnr04CZXmMhM6rlDFzK3kDRDUJ0DK", new DateTime(2025, 12, 5, 17, 18, 55, 168, DateTimeKind.Utc).AddTicks(5126) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(5872), "$2a$11$3Kpj.mvVjuyk1G1jYVIvDeA2BFqCkSJCcxPlzXxViDFCZXgSowEre", new DateTime(2025, 12, 5, 17, 18, 55, 288, DateTimeKind.Utc).AddTicks(5876) });
        }
    }
}
