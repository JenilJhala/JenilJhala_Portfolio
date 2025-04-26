using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmallUsedCars_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class PayrollUpdateComission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "AppointmentDate",
                value: new DateTime(2025, 3, 14, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7902));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440000",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "142ad1da-dab8-4f8d-9547-61013494568f", new DateTime(2020, 3, 19, 16, 22, 36, 187, DateTimeKind.Local).AddTicks(9802), new DateTime(2020, 3, 19, 16, 22, 36, 187, DateTimeKind.Local).AddTicks(9524), "AQAAAAIAAYagAAAAEPolkeO12YrQNh+6InL+uOt/JhMcYTlX50yjr3yYjSBxT2Y1hSb/NjmAxiJsoOjNKQ==", "fdd70beb-835e-4268-91cd-cb6c044ac8b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "768e2a71-58f1-4d44-9a61-1e41b0972b5a",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1cec2260-e19b-4bbc-b95e-f19e2642c593", new DateTime(2023, 3, 19, 16, 22, 36, 317, DateTimeKind.Local).AddTicks(7323), new DateTime(2022, 3, 19, 16, 22, 36, 317, DateTimeKind.Local).AddTicks(7129), "AQAAAAIAAYagAAAAEKK+Jf0tig4LbzXGf2eIU5+XzECk1gk9KAJw1G2OW0zHfPrcu1r1zYnxVLSlW05cmQ==", "5977aee8-7560-4045-b47c-231ea3f89aaa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dff31e3b-123a-48f9-942b-4d1b34a14e1c",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0fcce0b-b227-41b0-bb6d-1439ea59c81c", new DateTime(2021, 3, 19, 16, 22, 36, 410, DateTimeKind.Local).AddTicks(2570), new DateTime(2019, 3, 19, 16, 22, 36, 410, DateTimeKind.Local).AddTicks(2501), "AQAAAAIAAYagAAAAEGvbwFOryIAZnzSJx9Y8hFi2CBraabUf95+EGdg0pbMcBN178G39RxpnUzE1I1vMxg==", "846b8f5e-61f2-4508-a75f-2da51a9e5ced" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f47ac10b-58cc-4372-a567-0e02b2c3d479",
                columns: new[] { "ConcurrencyStamp", "CurrentPositionStartDate", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a7a089-2818-461b-9fdb-e12928d294f0", new DateTime(2023, 3, 19, 16, 22, 36, 2, DateTimeKind.Local).AddTicks(9395), new DateTime(2015, 3, 19, 16, 22, 36, 2, DateTimeKind.Local).AddTicks(9267), "AQAAAAIAAYagAAAAEGx8lJDpRayovoTEPWVkKVecIg6OQ/FSgai7cXClmobguBdJVPZ3epMjEOmbDDNtBQ==", "3fea383e-6587-4846-be9c-f9873889555d" });

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 1,
                column: "LastServiceDate",
                value: new DateTime(2024, 3, 19, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 2,
                column: "LastServiceDate",
                value: new DateTime(2023, 3, 19, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7758));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 3,
                column: "LastServiceDate",
                value: new DateTime(2025, 2, 2, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7783));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 4,
                column: "LastServiceDate",
                value: new DateTime(2020, 3, 19, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "MaintenanceRecords",
                keyColumn: "VehicleMaintenanceId",
                keyValue: 5,
                column: "LastServiceDate",
                value: new DateTime(2024, 12, 19, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7794));

            migrationBuilder.UpdateData(
                table: "PayrollRecords",
                keyColumn: "PayrollRecordId",
                keyValue: 1,
                column: "PayDate",
                value: new DateTime(2025, 2, 19, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "SalesRecords",
                keyColumn: "SalesRecordId",
                keyValue: 1,
                column: "SaleDate",
                value: new DateTime(2025, 2, 17, 16, 22, 36, 492, DateTimeKind.Local).AddTicks(7944));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 1,
                column: "PayDate",
                value: new DateTime(2025, 2, 19, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9582));

            migrationBuilder.UpdateData(
                table: "SalesRecords",
                keyColumn: "SalesRecordId",
                keyValue: 1,
                column: "SaleDate",
                value: new DateTime(2025, 2, 17, 14, 41, 45, 234, DateTimeKind.Local).AddTicks(9505));
        }
    }
}
