using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampus.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIoTSensorSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Sensors table if it doesn't exist
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS `Sensors` (
                    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
                    `SensorId` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
                    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
                    `Type` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                    `Location` varchar(200) CHARACTER SET utf8mb4 NULL,
                    `Status` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                    `Unit` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
                    `MinThreshold` decimal(10,2) NULL,
                    `MaxThreshold` decimal(10,2) NULL,
                    `IsActive` tinyint(1) NOT NULL,
                    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
                    `CreatedAt` datetime(6) NOT NULL,
                    `UpdatedAt` datetime(6) NOT NULL,
                    CONSTRAINT `PK_Sensors` PRIMARY KEY (`Id`)
                ) CHARACTER SET=utf8mb4;
            ");

            // Create SensorData table if it doesn't exist
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS `SensorData` (
                    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
                    `SensorId` char(36) COLLATE ascii_general_ci NOT NULL,
                    `Timestamp` datetime(6) NOT NULL,
                    `Value` decimal(10,2) NOT NULL,
                    `Unit` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
                    `IsAnomaly` tinyint(1) NOT NULL,
                    `AnomalyReason` varchar(500) CHARACTER SET utf8mb4 NULL,
                    `MetadataJson` text CHARACTER SET utf8mb4 NULL,
                    `CreatedAt` datetime(6) NOT NULL,
                    CONSTRAINT `PK_SensorData` PRIMARY KEY (`Id`),
                    CONSTRAINT `FK_SensorData_Sensors_SensorId` FOREIGN KEY (`SensorId`) REFERENCES `Sensors` (`Id`) ON DELETE CASCADE
                ) CHARACTER SET=utf8mb4;
            ");

            // Create indexes if they don't exist (MySQL doesn't support IF NOT EXISTS for indexes, so we use a workaround)
            migrationBuilder.Sql(@"
                SET @exist := (SELECT COUNT(*) FROM information_schema.statistics 
                    WHERE table_schema = DATABASE() 
                    AND table_name = 'Sensors' 
                    AND index_name = 'IX_Sensors_SensorId');
                SET @sqlstmt := IF(@exist = 0, 'CREATE UNIQUE INDEX `IX_Sensors_SensorId` ON `Sensors` (`SensorId`)', 'SELECT ''Index already exists''');
                PREPARE stmt FROM @sqlstmt;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            ");

            migrationBuilder.Sql(@"
                SET @exist := (SELECT COUNT(*) FROM information_schema.statistics 
                    WHERE table_schema = DATABASE() 
                    AND table_name = 'Sensors' 
                    AND index_name = 'IX_Sensors_Type_Status');
                SET @sqlstmt := IF(@exist = 0, 'CREATE INDEX `IX_Sensors_Type_Status` ON `Sensors` (`Type`, `Status`)', 'SELECT ''Index already exists''');
                PREPARE stmt FROM @sqlstmt;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            ");

            migrationBuilder.Sql(@"
                SET @exist := (SELECT COUNT(*) FROM information_schema.statistics 
                    WHERE table_schema = DATABASE() 
                    AND table_name = 'Sensors' 
                    AND index_name = 'IX_Sensors_Location');
                SET @sqlstmt := IF(@exist = 0, 'CREATE INDEX `IX_Sensors_Location` ON `Sensors` (`Location`)', 'SELECT ''Index already exists''');
                PREPARE stmt FROM @sqlstmt;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            ");

            migrationBuilder.Sql(@"
                SET @exist := (SELECT COUNT(*) FROM information_schema.statistics 
                    WHERE table_schema = DATABASE() 
                    AND table_name = 'SensorData' 
                    AND index_name = 'IX_SensorData_SensorId_Timestamp');
                SET @sqlstmt := IF(@exist = 0, 'CREATE INDEX `IX_SensorData_SensorId_Timestamp` ON `SensorData` (`SensorId`, `Timestamp`)', 'SELECT ''Index already exists''');
                PREPARE stmt FROM @sqlstmt;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            ");

            migrationBuilder.Sql(@"
                SET @exist := (SELECT COUNT(*) FROM information_schema.statistics 
                    WHERE table_schema = DATABASE() 
                    AND table_name = 'SensorData' 
                    AND index_name = 'IX_SensorData_Timestamp');
                SET @sqlstmt := IF(@exist = 0, 'CREATE INDEX `IX_SensorData_Timestamp` ON `SensorData` (`Timestamp`)', 'SELECT ''Index already exists''');
                PREPARE stmt FROM @sqlstmt;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            ");

            migrationBuilder.Sql(@"
                SET @exist := (SELECT COUNT(*) FROM information_schema.statistics 
                    WHERE table_schema = DATABASE() 
                    AND table_name = 'SensorData' 
                    AND index_name = 'IX_SensorData_IsAnomaly');
                SET @sqlstmt := IF(@exist = 0, 'CREATE INDEX `IX_SensorData_IsAnomaly` ON `SensorData` (`IsAnomaly`)', 'SELECT ''Index already exists''');
                PREPARE stmt FROM @sqlstmt;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            ");

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7864), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7864) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7864), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7864) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7952), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7952) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae010008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7952), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7952) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7978), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7978) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae020008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7978), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(7978) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8030), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8030) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae030008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8030), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8030) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8048) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae040008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8048) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8065), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8065) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae050008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8065), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8065) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8081), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8081) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae060008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8081), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8081) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8099), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8099) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae070008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8099), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8099) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8141), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8141) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae080008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8141), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8141) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8162), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8162) });

            migrationBuilder.UpdateData(
                table: "AcademicEvents",
                keyColumn: "Id",
                keyValue: new Guid("ae090008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8162), new DateTime(2025, 12, 27, 9, 10, 11, 106, DateTimeKind.Utc).AddTicks(8162) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(2434), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(2533) });

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: new Guid("caf22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(2621) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c1a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1619), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1720) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c2a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1814), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1814) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c3a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1817), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1818) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c4a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1821), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1821) });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: new Guid("c5a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1824), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(1824) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(7908), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(7998) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8091), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8091) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8095), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8095) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8107), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8107) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8110), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8111) });

            migrationBuilder.UpdateData(
                table: "CourseSections",
                keyColumn: "Id",
                keyValue: new Guid("5ec00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8114), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(8114) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4424), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4601) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4708), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4708) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4711), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4712) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4716), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4717) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4719), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4722), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4723) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4725), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4725) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4728), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(4729) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 11, DateTimeKind.Utc).AddTicks(9701), new DateTime(2025, 12, 27, 9, 10, 10, 11, DateTimeKind.Utc).AddTicks(9809) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 11, DateTimeKind.Utc).AddTicks(9901), new DateTime(2025, 12, 27, 9, 10, 10, 11, DateTimeKind.Utc).AddTicks(9902) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 11, DateTimeKind.Utc).AddTicks(9904), new DateTime(2025, 12, 27, 9, 10, 10, 11, DateTimeKind.Utc).AddTicks(9905) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9381), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9153), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9479) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9568), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9566), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9569) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9595), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9594), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9595) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9599), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9598), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9599) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9603), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9602), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9603) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9606), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9606), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9607) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9609), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9610) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9613), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9613), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9614) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9617), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9617), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9617) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9621) });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: new Guid("e0a00011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "EnrollmentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9626), new DateTime(2025, 11, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9625), new DateTime(2025, 12, 27, 9, 10, 11, 105, DateTimeKind.Utc).AddTicks(9626) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e11111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(4759), new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 21, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(4835) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(4915), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 31, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(4913), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(4915) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(5241), new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 4, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(4926), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(5242) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e44444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(5249), new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 2, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(5248), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(5249) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 491, DateTimeKind.Utc).AddTicks(3340), new DateTime(2025, 12, 27, 9, 10, 10, 491, DateTimeKind.Utc).AddTicks(3452) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("fa222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 491, DateTimeKind.Utc).AddTicks(3595), new DateTime(2025, 12, 27, 9, 10, 10, 491, DateTimeKind.Utc).AddTicks(3596) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00001-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6629), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6705) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00002-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6786), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6787) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00003-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6790), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6790) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00011-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6794), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6795) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00012-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6798), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6798) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00013-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6801), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6801) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00021-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6808), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6808) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00022-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6811), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6812) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00023-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6815), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6815) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00031-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6818), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6819) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00032-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6821), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6822) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00033-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6825), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6825) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00041-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6828), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6828) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00042-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6831), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6831) });

            migrationBuilder.UpdateData(
                table: "MealMenus",
                keyColumn: "Id",
                keyValue: new Guid("aaa00043-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6836), new DateTime(2025, 12, 27, 9, 10, 11, 108, DateTimeKind.Utc).AddTicks(6836) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000001-0001-0001-0001-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(790), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(867) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000002-0002-0002-0002-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(950), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(951) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000003-0003-0003-0003-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(971), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(971) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000004-0004-0004-0004-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(974), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(975) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000005-0005-0005-0005-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(978), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(978) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000006-0006-0006-0006-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(981), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(982) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000007-0007-0007-0007-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(985), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(985) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000008-0008-0008-0008-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(992), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(992) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000009-0009-0009-0009-000000000009"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(996), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(996) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000010-0010-0010-0010-000000000010"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(1012), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(1012) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("5c000011-0011-0011-0011-000000000011"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(1016), new DateTime(2025, 12, 27, 9, 10, 11, 107, DateTimeKind.Utc).AddTicks(1016) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5004), new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5104) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5243), new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5243) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5279), new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5279) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5284), new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5284) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5287), new DateTime(2025, 12, 27, 9, 10, 11, 102, DateTimeKind.Utc).AddTicks(5288) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 211, DateTimeKind.Utc).AddTicks(5621), "$2a$11$m7y8q1krENcgfOlHg.Q19uw7Xc1r98Pqe0jBk/LcJNqGpCBLtQJly", new DateTime(2025, 12, 27, 9, 10, 10, 211, DateTimeKind.Utc).AddTicks(5778) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 612, DateTimeKind.Utc).AddTicks(7724), "$2a$11$F5YlvY04iC5Cr.enfh92n.ICaWaKGLOf2RDuDFX1PCklgM/U9CWMG", new DateTime(2025, 12, 27, 9, 10, 10, 612, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 738, DateTimeKind.Utc).AddTicks(2442), "$2a$11$QWTIEUE/KJhquTLImk4cU.9vZCrEwyXLyW6Kspe6Zrr7HS6dDSA8y", new DateTime(2025, 12, 27, 9, 10, 10, 738, DateTimeKind.Utc).AddTicks(2447) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 862, DateTimeKind.Utc).AddTicks(7466), "$2a$11$QNKrNcCj1nC5pGmQ4DszfO3RX1oEIq6g4XHLMmDKVoIDdp0GiaZnm", new DateTime(2025, 12, 27, 9, 10, 10, 862, DateTimeKind.Utc).AddTicks(7472) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 984, DateTimeKind.Utc).AddTicks(8857), "$2a$11$sm9VPRAxT0l8vdKCRB9uD.eBAFEMGcjgwmPJUFZjs2ZrAM.k2//62", new DateTime(2025, 12, 27, 9, 10, 10, 984, DateTimeKind.Utc).AddTicks(8924) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 11, 101, DateTimeKind.Utc).AddTicks(1974), "$2a$11$tLCaDVAymkDs.MBvGdXwKul6M7nBbprDmGy1HczYHf7odzIdacPWW", new DateTime(2025, 12, 27, 9, 10, 11, 101, DateTimeKind.Utc).AddTicks(1978) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 368, DateTimeKind.Utc).AddTicks(7034), "$2a$11$S3tanKRmz9j.xj3e77PhjuxZqs9yNwnptOdWFTSCH4s8c3zAMxnm6", new DateTime(2025, 12, 27, 9, 10, 10, 368, DateTimeKind.Utc).AddTicks(7040) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 27, 9, 10, 10, 490, DateTimeKind.Utc).AddTicks(9769), "$2a$11$FyU1cM4ceFpy7SbfSzDuLenJEWqhAUgE/VQVz2QOySrf.uNLJAQqa", new DateTime(2025, 12, 27, 9, 10, 10, 490, DateTimeKind.Utc).AddTicks(9773) });

            // Indexes are created via SQL above with existence checks
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");

            migrationBuilder.DropTable(
                name: "Sensors");

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
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3163), new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 17, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(2668), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3266) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e22222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3370), new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 27, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3369), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3371) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e33333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3794), new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 31, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3375), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3795) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e0e44444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "Date", "RegistrationDeadline", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3803), new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 29, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3802), new DateTime(2025, 12, 23, 21, 18, 7, 373, DateTimeKind.Utc).AddTicks(3803) });

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
        }
    }
}
