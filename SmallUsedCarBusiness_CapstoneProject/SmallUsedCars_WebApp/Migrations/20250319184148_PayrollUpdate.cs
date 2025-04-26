using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmallUsedCars_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class PayrollUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Commission",
                table: "PayrollRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "PayrollRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRate",
                table: "PayrollRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "AppointmentDate",
                value: new DateTime(2025, 3, 14, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9410));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440000",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ed0671e-9b9a-464f-a536-211bd3dfe05c", new DateTime(2020, 3, 19, 14, 41, 44, 654, DateTimeKind.Local).AddTicks(6326), new DateTime(2020, 3, 19, 14, 41, 44, 654, DateTimeKind.Local).AddTicks(6252), "AQAAAAIAAYagAAAAECVAjjZuBHRf+oTp3BZ2jHWRJ3PeV9PZwbxUxJ96K4WCwSvDpBTTFKBwR+ZjeMjzfA==", "1137f162-218f-4e60-bd75-9f0d62eda6bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "768e2a71-58f1-4d44-9a61-1e41b0972b5a",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9b1e1ec-edea-4354-9a06-7a25dd4b2a35", new DateTime(2023, 3, 19, 14, 41, 44, 888, DateTimeKind.Local).AddTicks(7219), new DateTime(2022, 3, 19, 14, 41, 44, 888, DateTimeKind.Local).AddTicks(7140), "AQAAAAIAAYagAAAAEPm4B6VELfYfdW2/UVnN5tU/irIYIQ5/rOmd2RnlQLRqAsyxnAyx/v+seiuIQAFqxg==", "c5fd2542-721a-408d-aab2-d34b24a4a64f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dff31e3b-123a-48f9-942b-4d1b34a14e1c",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f374b855-087d-4063-906e-91eae89bb0c2", new DateTime(2021, 3, 19, 14, 41, 45, 56, DateTimeKind.Local).AddTicks(6283), new DateTime(2019, 3, 19, 14, 41, 45, 56, DateTimeKind.Local).AddTicks(6184), "AQAAAAIAAYagAAAAEFwiI+AbZbDBoYl/NUzirXW9xZujuSWwtqwxJEdE0bCLl/u8LqgNAOmni9B01LHAeg==", "c947e590-1c53-471a-b90b-ea87ca8981cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f47ac10b-58cc-4372-a567-0e02b2c3d479",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c628bc21-e7db-43bd-a59f-b9c12833d514", new DateTime(2023, 3, 19, 14, 41, 44, 354, DateTimeKind.Local).AddTicks(5459), new DateTime(2015, 3, 19, 14, 41, 44, 354, DateTimeKind.Local).AddTicks(5324), "AQAAAAIAAYagAAAAEOs2RwVAIThRLZZMunyYIhuUCZzVG/SnGSNADVUik3w9y/qejXp/R0sIqLUwrZnBMA==", "f6d48bc4-ffb1-4c5c-87c5-b69b5d6c4800" });

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 1,
                column: "LastServiceDate",
                value: new DateTime(2024, 3, 19, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9047));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 2,
                column: "LastServiceDate",
                value: new DateTime(2023, 3, 19, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9147));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 3,
                column: "LastServiceDate",
                value: new DateTime(2025, 2, 2, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9168));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 4,
                column: "LastServiceDate",
                value: new DateTime(2020, 3, 19, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9181));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 5,
                column: "LastServiceDate",
                value: new DateTime(2024, 12, 19, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -40,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -39,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -38,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -37,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -36,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -35,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -34,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -28,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -27,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -26,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -25,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -24,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -17,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -16,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -15,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -14,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -13,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -12,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -11,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -10,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -9,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -8,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -7,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -6,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -5,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -4,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -3,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -2,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: -1,
                columns: new[] { "Commission", "SalePrice", "TaxRate" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: 1,
                columns: new[] { "Commission", "PayDate", "SalePrice", "TaxRate" },
                values: new object[] { 0m, new DateTime(2025, 2, 19, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9582), 0m, 0m });

            migrationBuilder.UpdateData(
                table: "SalesRecords",
                keyColumn: "SalesRecordId",
                keyValue: 1,
                column: "SaleDate",
                value: new DateTime(2025, 2, 17, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9505));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commission",
                table: "PayrollRecords");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "PayrollRecords");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "PayrollRecords");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "AppointmentDate",
                value: new DateTime(2025, 3, 13, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440000",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41fdb60e-964b-4e9d-9c50-3b7b3588f1aa", new DateTime(2020, 3, 18, 23, 58, 5, 384, DateTimeKind.Local).AddTicks(3936), new DateTime(2020, 3, 18, 23, 58, 5, 384, DateTimeKind.Local).AddTicks(3861), "AQAAAAIAAYagAAAAEDMTX7Xxf+Y/p+OPPc2QSEMe4oEyTZV7xRCoE+9jGarWQwl/ckHTMDLFDnZhXe3lxg==", "db6153d4-bd10-46eb-ab86-50293d1bc868" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "768e2a71-58f1-4d44-9a61-1e41b0972b5a",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb5af7cc-0e18-4393-b7db-f5cf48677f35", new DateTime(2023, 3, 18, 23, 58, 5, 479, DateTimeKind.Local).AddTicks(4354), new DateTime(2022, 3, 18, 23, 58, 5, 479, DateTimeKind.Local).AddTicks(4278), "AQAAAAIAAYagAAAAEKL1QcgORQcnObAM0/RDZhk8+tJE5FNfd0rAPMfN+NSgLpVWPfIzQoNux/qiln0btA==", "22f67214-f4f1-4bde-b6cf-894a1f11dad4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dff31e3b-123a-48f9-942b-4d1b34a14e1c",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbdfc71d-298d-49ef-b592-2a297179ed31", new DateTime(2021, 3, 18, 23, 58, 5, 567, DateTimeKind.Local).AddTicks(9955), new DateTime(2019, 3, 18, 23, 58, 5, 567, DateTimeKind.Local).AddTicks(9878), "AQAAAAIAAYagAAAAEO5YSdOiIOsRSUmUxY8j6NE8/5qJGDgOMt8cS/q1Fy6Hv0f/cOKWdQk5OA18N0uMEA==", "25ba3712-61a2-41a3-b9d0-f6e76766a73f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f47ac10b-58cc-4372-a567-0e02b2c3d479",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "618f68ab-63e9-4087-bbed-364c7c05e6e5", new DateTime(2023, 3, 18, 23, 58, 5, 310, DateTimeKind.Local).AddTicks(3646), new DateTime(2015, 3, 18, 23, 58, 5, 310, DateTimeKind.Local).AddTicks(3575), "AQAAAAIAAYagAAAAEBaiAA6BOb/Y5rRiWEcRWBNIjda0BZo7nPu7CLdgBDDFqVchIDaxXsY/sxqDfz8w3Q==", "1b7c28ac-c490-43e3-8b80-ad875f25178f" });

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 1,
                column: "LastServiceDate",
                value: new DateTime(2024, 3, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4402));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 2,
                column: "LastServiceDate",
                value: new DateTime(2023, 3, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4492));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 3,
                column: "LastServiceDate",
                value: new DateTime(2025, 2, 1, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4500));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 4,
                column: "LastServiceDate",
                value: new DateTime(2020, 3, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4509));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 5,
                column: "LastServiceDate",
                value: new DateTime(2024, 12, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4512));

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: 1,
                column: "PayDate",
                value: new DateTime(2025, 2, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4729));

            migrationBuilder.UpdateData(
                table: "SalesRecords",
                keyColumn: "SalesRecordId",
                keyValue: 1,
                column: "SaleDate",
                value: new DateTime(2025, 2, 16, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4669));
        }
    }
}
