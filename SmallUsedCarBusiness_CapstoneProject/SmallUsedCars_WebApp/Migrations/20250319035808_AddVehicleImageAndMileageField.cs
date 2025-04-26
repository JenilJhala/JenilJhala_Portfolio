using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmallUsedCars_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleImageAndMileageField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentPositionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    SalesRecordId = table.Column<int>(type: "int", nullable: false),
                    HRId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRs",
                columns: table => new
                {
                    HRId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRs", x => x.HRId);
                    table.ForeignKey(
                        name: "FK_HRs_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayrollRecords",
                columns: table => new
                {
                    PayrollRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalBeforeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SalesRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollRecords", x => x.PayrollRecordId);
                    table.ForeignKey(
                        name: "FK_PayrollRecords_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mileage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Powertrain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StockOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventories_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceAlerts",
                columns: table => new
                {
                    VehicleMaintenanceAlertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceAlerts", x => x.VehicleMaintenanceAlertId);
                    table.ForeignKey(
                        name: "FK_MaintenanceAlerts_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRecords",
                columns: table => new
                {
                    VehicleMaintenanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaintenanceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.VehicleMaintenanceId);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesRecords",
                columns: table => new
                {
                    SalesRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionEarned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    PayrollRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRecords", x => x.SalesRecordId);
                    table.ForeignKey(
                        name: "FK_SalesRecords_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesRecords_PayrollRecords_PayrollRecordId",
                        column: x => x.PayrollRecordId,
                        principalTable: "PayrollRecords",
                        principalColumn: "PayrollRecordId");
                    table.ForeignKey(
                        name: "FK_SalesRecords_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTransactions",
                columns: table => new
                {
                    VehicleTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TradeInValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarginRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTransactions", x => x.VehicleTransactionId);
                    table.ForeignKey(
                        name: "FK_VehicleTransactions_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VehicleTransactions_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AppointmentId", "ConcurrencyStamp", "ContactNumber", "CurrentPositionStartDate", "Department", "Email", "EmailConfirmed", "EmployeeName", "HRId", "HireDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Position", "SalesRecordId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "550e8400-e29b-41d4-a716-446655440000", 0, 0, "41fdb60e-964b-4e9d-9c50-3b7b3588f1aa", "288-433-4432", new DateTime(2020, 3, 18, 23, 58, 5, 384, DateTimeKind.Local).AddTicks(3936), "Sales", "angelina@example.com", true, "Angelina Jones", 0, new DateTime(2020, 3, 18, 23, 58, 5, 384, DateTimeKind.Local).AddTicks(3861), false, null, "ANGELINA@EXAMPLE.COM", "ANGELINAJ", "AQAAAAIAAYagAAAAEDMTX7Xxf+Y/p+OPPc2QSEMe4oEyTZV7xRCoE+9jGarWQwl/ckHTMDLFDnZhXe3lxg==", null, false, "Salesperson", 0, "db6153d4-bd10-46eb-ab86-50293d1bc868", false, "angelinaj" },
                    { "768e2a71-58f1-4d44-9a61-1e41b0972b5a", 0, 0, "bb5af7cc-0e18-4393-b7db-f5cf48677f35", "647-872-9432", new DateTime(2023, 3, 18, 23, 58, 5, 479, DateTimeKind.Local).AddTicks(4354), "Maintenance", "sarah@example.com", true, "Sarah Connor", 0, new DateTime(2022, 3, 18, 23, 58, 5, 479, DateTimeKind.Local).AddTicks(4278), false, null, "SARAH@EXAMPLE.COM", "SARAHCONNOR", "AQAAAAIAAYagAAAAEKL1QcgORQcnObAM0/RDZhk8+tJE5FNfd0rAPMfN+NSgLpVWPfIzQoNux/qiln0btA==", null, false, "Technician", 0, "22f67214-f4f1-4bde-b6cf-894a1f11dad4", false, "sarahconnor" },
                    { "dff31e3b-123a-48f9-942b-4d1b34a14e1c", 0, 0, "bbdfc71d-298d-49ef-b592-2a297179ed31", "403-555-7832", new DateTime(2021, 3, 18, 23, 58, 5, 567, DateTimeKind.Local).AddTicks(9955), "Finance", "james@example.com", true, "James Roberts", 0, new DateTime(2019, 3, 18, 23, 58, 5, 567, DateTimeKind.Local).AddTicks(9878), false, null, "JAMES@EXAMPLE.COM", "JAMESROBERTS", "AQAAAAIAAYagAAAAEO5YSdOiIOsRSUmUxY8j6NE8/5qJGDgOMt8cS/q1Fy6Hv0f/cOKWdQk5OA18N0uMEA==", null, false, "Finance Manager", 0, "25ba3712-61a2-41a3-b9d0-f6e76766a73f", false, "jamesroberts" },
                    { "f47ac10b-58cc-4372-a567-0e02b2c3d479", 0, 0, "618f68ab-63e9-4087-bbed-364c7c05e6e5", "541-222-3574", new DateTime(2023, 3, 18, 23, 58, 5, 310, DateTimeKind.Local).AddTicks(3646), "Sales", "matt@example.com", true, "Matt Smith", 0, new DateTime(2015, 3, 18, 23, 58, 5, 310, DateTimeKind.Local).AddTicks(3575), false, null, "MATT@EXAMPLE.COM", "MATTSMITH", "AQAAAAIAAYagAAAAEBaiAA6BOb/Y5rRiWEcRWBNIjda0BZo7nPu7CLdgBDDFqVchIDaxXsY/sxqDfz8w3Q==", null, false, "Manager", 0, "1b7c28ac-c490-43e3-8b80-ad875f25178f", false, "mattsmith" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "City", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode", "Province", "Street", "UnitNumber" },
                values: new object[,]
                {
                    { 1, "Toronto", "jenil.rashmi@gmail.com", "David", "Johnson", "573-486-7190", "M5H1B6", "Ontario", "Queen St", "10A" },
                    { 2, "Vancouver", "michael@email.com", "Michael", "Jackson", "942-654-3210", "V5R5H6", "British Columbia", "Kingsway", "5B" }
                });

            migrationBuilder.InsertData(
                table: "PayrollRecords",
                columns: new[] { "PayrollRecordId", "BaseSalary", "EmployeeId", "PayDate", "SalesRecordId", "Tax", "TotalBeforeTax", "TotalPay" },
                values: new object[,]
                {
                    { -40, 7100m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 639m, 7100m, 6461m },
                    { -39, 6900m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 621m, 6900m, 6279m },
                    { -38, 6700m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 603m, 6700m, 6097m },
                    { -37, 6500m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 585m, 6500m, 5915m },
                    { -36, 6300m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 567m, 6300m, 5733m },
                    { -35, 6100m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 549m, 6100m, 5551m },
                    { -34, 5800m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 522m, 5800m, 5278m },
                    { -28, 6700m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 603m, 6700m, 6097m },
                    { -27, 6500m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 585m, 6500m, 5915m },
                    { -26, 6300m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 567m, 6300m, 5733m },
                    { -25, 6100m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 549m, 6100m, 5551m },
                    { -24, 5800m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 522m, 5800m, 5278m },
                    { -17, 6700m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 603m, 6700m, 6097m },
                    { -16, 6500m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 585m, 6500m, 5915m },
                    { -15, 6300m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 567m, 6300m, 5733m },
                    { -14, 6000m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 540m, 6000m, 5460m },
                    { -13, 5800m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 522m, 5800m, 5278m },
                    { -12, 5500m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 495m, 5500m, 5005m },
                    { -11, 5250m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 473m, 5250m, 4777m },
                    { -10, 5000m, "550e8400-e29b-41d4-a716-446655440000", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 450m, 5000m, 4550m },
                    { -9, 6200m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 558m, 6200m, 5642m },
                    { -8, 6000m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 540m, 6000m, 5460m },
                    { -7, 5800m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 522m, 5800m, 5278m },
                    { -6, 5600m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 504m, 5600m, 5096m },
                    { -5, 5400m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 486m, 5400m, 4914m },
                    { -4, 5150m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 464m, 5150m, 4686m },
                    { -3, 4900m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 441m, 4900m, 4459m },
                    { -2, 4700m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 423m, 4700m, 4277m },
                    { -1, 4500m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 405m, 4500m, 4095m },
                    { 1, 5000m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", new DateTime(2025, 2, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4729), 0, 500m, 5500m, 5000m }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "CustomerId", "ImageFileName", "Manufacturer", "MarketValue", "Mileage", "Model", "PlateNumber", "Powertrain", "Status", "Type", "Vin", "Year" },
                values: new object[,]
                {
                    { 1, 1, "2022Accord.jpg", "Honda", 22000m, "48000km", "Accord", "ABC123", "Gasoline", "Available", "Sedan", "1HGCM82633A123456", 2022 },
                    { 2, 2, "2023CR-V.jpg", "Honda", 28000m, "2500km", "CR-V", "XYZ789", "Hybrid", "Available", "SUV", "2HGFA16548H123456", 2023 },
                    { 3, 1, "2016Focus.jpg", "Ford", 20000m, "52000km", "Focus", "DEF456", "Gasoline", "Sold", "Hatchback", "3FADP4BJ2KM123456", 2016 },
                    { 4, 2, "2020Camry.jpg", "Toyota", 25000m, "33000km", "Camry", "GHI789", "Hybrid", "Available", "Sedan", "4T1BF1FK1HU123456", 2020 },
                    { 5, 1, "2023Model3.jpg", "Tesla", 35000m, "7000km", "Model 3", "JKL012", "Electric", "Available", "Sedan", "5YJ3E1EA7LF123456", 2023 },
                    { 6, 2, "2022Explorer.jpg", "Ford", 26500m, "59300km", "Explorer", "LMN456", "Gasoline", "Sold", "SUV", "JH4KA8260NC983245", 2022 },
                    { 7, 1, "2023Silverado.jpg", "Chevrolet", 37000m, "11000km", "Silverado", "QWE789", "Diesel", "Under Repair", "Truck", "3GCUKREC1JG491275", 2023 },
                    { 8, 2, "2021X5.jpg", "BMW", 39000m, "26400km", "X5", "RTY852", "Hybrid", "Available", "SUV", "WBXHT3C36J5F81234", 2021 },
                    { 9, 1, "2023Altima.jpg", "Nissan", 19500m, "42000km", "Altima", "UIO369", "Gasoline", "Available", "Sedan", "JN1CV6EL7MM975312", 2023 },
                    { 10, 2, "2024Sonata.jpg", "Hyundai", 22000m, "1000km", "Sonata", "PAS741", "Hybrid", "Available", "Sedan", "KMHDH4AE3EU653948", 2024 },
                    { 11, 1, "2022Sportage.jpg", "Kia", 28500m, "2000km", "Sportage", "GHJ654", "Electric", "Available", "SUV", "KNDPM3AC2H7128394", 2022 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "AppointmentType", "CustomerId", "Description", "EmployeeId", "Status", "VehicleId" },
                values: new object[] { 1, new DateTime(2025, 3, 13, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4631), "Service", 1, "Oil Change", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "Completed", 1 });

            migrationBuilder.InsertData(
                table: "MaintenanceRecords",
                columns: new[] { "VehicleMaintenanceId", "Cost", "LastServiceDate", "MaintenanceDescription", "MaintenanceType", "TaxAmount", "TotalAmount", "VehicleId" },
                values: new object[,]
                {
                    { 1, 50m, new DateTime(2024, 3, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4402), "Routine oil change", "Oil Change", 5m, 55m, 1 },
                    { 2, 200m, new DateTime(2023, 3, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4492), "Replaced worn-out brake pads", "Brake Pad Replacement", 20m, 220m, 1 },
                    { 3, 40m, new DateTime(2025, 2, 1, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4500), "Routine tire rotation for even wear", "Tire Rotation", 4m, 44m, 2 },
                    { 4, 150m, new DateTime(2020, 3, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4509), "Replaced old car battery", "Battery Replacement", 15m, 165m, 2 },
                    { 5, 300m, new DateTime(2024, 12, 18, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4512), "Complete engine diagnostic and tune-up", "Engine Tune-Up", 30m, 330m, 1 }
                });

            migrationBuilder.InsertData(
                table: "SalesRecords",
                columns: new[] { "SalesRecordId", "CommissionEarned", "CommissionRate", "EmployeeId", "PayrollRecordId", "SaleDate", "SalePrice", "VehicleId" },
                values: new object[,]
                {
                    { -11, 0m, 0.05m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", null, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, 11 },
                    { -10, 0m, 0.05m, "dff31e3b-123a-48f9-942b-4d1b34a14e1c", null, new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, 10 },
                    { -9, 0m, 0.05m, "768e2a71-58f1-4d44-9a61-1e41b0972b5a", null, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2570m, 9 },
                    { -8, 0m, 0.05m, "768e2a71-58f1-4d44-9a61-1e41b0972b5a", null, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2850m, 8 },
                    { -7, 0m, 0.05m, "768e2a71-58f1-4d44-9a61-1e41b0972b5a", null, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2550m, 7 },
                    { -6, 0m, 0.05m, "768e2a71-58f1-4d44-9a61-1e41b0972b5a", null, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000m, 6 },
                    { -5, 0m, 0.05m, "550e8400-e29b-41d4-a716-446655440000", null, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1700m, 5 },
                    { -4, 0m, 0.05m, "550e8400-e29b-41d4-a716-446655440000", null, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 900m, 4 },
                    { -3, 0m, 0.05m, "550e8400-e29b-41d4-a716-446655440000", null, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000m, 3 },
                    { -2, 0m, 0.05m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", null, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500m, 2 },
                    { -1, 0m, 0.05m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", null, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200m, 1 },
                    { 1, 1100m, 0.05m, "f47ac10b-58cc-4372-a567-0e02b2c3d479", null, new DateTime(2025, 2, 16, 23, 58, 5, 637, DateTimeKind.Local).AddTicks(4669), 22000m, 1 }
                });

            migrationBuilder.InsertData(
                table: "VehicleTransactions",
                columns: new[] { "VehicleTransactionId", "EmployeeId", "FinalPrice", "MarginRate", "PurchasePrice", "SalesPrice", "TradeInValue", "TransactionDate", "TransactionType", "VehicleId" },
                values: new object[,]
                {
                    { 1, "f47ac10b-58cc-4372-a567-0e02b2c3d479", 26000m, 5.5m, 20000m, 26000m, 3000m, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sell", 1 },
                    { 2, "768e2a71-58f1-4d44-9a61-1e41b0972b5a", 22500m, 4.8m, 18000m, 22500m, 2500m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Buy", 2 },
                    { 3, "550e8400-e29b-41d4-a716-446655440000", 36000m, 6.2m, 28000m, 36000m, 5000m, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "TradeIn", 4 },
                    { 4, "768e2a71-58f1-4d44-9a61-1e41b0972b5a", 56000m, 5.8m, 47000m, 56000m, 6000m, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sell", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VehicleId",
                table: "Appointments",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HRs_EmployeeId",
                table: "HRs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_VehicleId",
                table: "Inventories",
                column: "VehicleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceAlerts_VehicleId",
                table: "MaintenanceAlerts",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_VehicleId",
                table: "MaintenanceRecords",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollRecords_EmployeeId",
                table: "PayrollRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRecords_EmployeeId",
                table: "SalesRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRecords_PayrollRecordId",
                table: "SalesRecords",
                column: "PayrollRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRecords_VehicleId",
                table: "SalesRecords",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransactions_EmployeeId",
                table: "VehicleTransactions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransactions_VehicleId",
                table: "VehicleTransactions",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HRs");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "MaintenanceAlerts");

            migrationBuilder.DropTable(
                name: "MaintenanceRecords");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SalesRecords");

            migrationBuilder.DropTable(
                name: "VehicleTransactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PayrollRecords");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
