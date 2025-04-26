using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Models;
using System.Reflection.Emit;

namespace SmallUsedCars_WebApp.Database
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Identity Tables (Do NOT explicitly define Identity-related DbSets)
        // IdentityUser를 상속받은 Employee는 AspNetUsers 테이블에 저장되므로 Employees DbSet을 정의하지 않음

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<HR> HRs { get; set; }
        public DbSet<PayrollRecord> PayrollRecords { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleMaintenance> MaintenanceRecords { get; set; }
        public DbSet<VehicleMaintenanceAlert> MaintenanceAlerts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<VehicleTransaction> VehicleTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Identity 설정 - Employee 엔터티를 AspNetUsers 테이블과 매핑
            builder.Entity<Employee>().ToTable("AspNetUsers");

            // Identity 기본 키 설정 (필수)
            builder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            builder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // 고객의 예약 생성시 필수 입력필드를 검사대상에서 제외하기 위해
            //  VehicleId를 NULL 허용 (Fluent API 사용)
            builder.Entity<Appointment>()
                .Property(a => a.VehicleId)
                .IsRequired(false);

            builder.Entity<Appointment>()
                .Property(a => a.EmployeeId)
                .IsRequired(false);

            builder.Entity<Notification>()
            .HasKey(n => n.NotificationId);

            builder.Entity<PayrollRecord>()
            .Property(pr => pr.EmployeeId)
            .IsRequired(false);

            //  Employee 관계 설정 (EmployeeId1 문제 해결)
            builder.Entity<VehicleTransaction>()
                .HasOne(vt => vt.Employee)
                .WithMany(e => e.VehicleTransactions)  //  Employee가 여러 VehicleTransactions을 가질 수 있도록 설정
                .HasForeignKey(vt => vt.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);  // 직원 삭제 시 FK를 NULL로 설정

            //  Vehicle 관계 설정
            builder.Entity<VehicleTransaction>()
                .HasOne(vt => vt.Vehicle)
                .WithMany(v => v.Transactions)
                .HasForeignKey(vt => vt.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);




            // ON DELETE SET NULL 설정
            builder.Entity<PayrollRecord>()
                .HasOne(pr => pr.Employee)
                .WithMany(e => e.PayrollRecords)
                .HasForeignKey(pr => pr.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);  // 직원 삭제 시, EmployeeId를 NULL로 설정


            // Relationships 설정
            builder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId);

            builder.Entity<Appointment>()
                .HasOne(a => a.Vehicle)
                .WithMany()
                .HasForeignKey(a => a.VehicleId);

            builder.Entity<Appointment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SalesRecord>()
                .HasOne(sr => sr.Employee)
                .WithMany(e => e.SalesRecords)
                .HasForeignKey(sr => sr.EmployeeId);

            builder.Entity<SalesRecord>()
                .HasOne(sr => sr.Vehicle)
                .WithMany()
                .HasForeignKey(sr => sr.VehicleId);

            builder.Entity<HR>()
                .HasOne(h => h.Employee)
                .WithMany(e => e.HRRecords)
                .HasForeignKey(h => h.EmployeeId);

            builder.Entity<PayrollRecord>()
                .HasOne(pr => pr.Employee)
                .WithMany(e => e.PayrollRecords)
                .HasForeignKey(pr => pr.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Inventory>()
                .HasOne(i => i.Vehicle)
                .WithOne(v => v.Inventory)
                .HasForeignKey<Inventory>(i => i.VehicleId)
                .IsRequired(false);

            builder.Entity<Vehicle>()
                .Property(v => v.CustomerId)
                .IsRequired(false);

            builder.Entity<VehicleMaintenance>()
                .HasOne(vm => vm.Vehicle)
                .WithMany(v => v.MaintenanceRecords)
                .HasForeignKey(vm => vm.VehicleId);

            builder.Entity<VehicleMaintenanceAlert>()
                .HasOne(vma => vma.Vehicle)
                .WithMany()
                .HasForeignKey(vma => vma.VehicleId);

            

            SeedData(builder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // 초기 Employee 계정 생성
            var hasher = new PasswordHasher<Employee>();

            var admin1 = new Employee
            {
                Id = "f47ac10b-58cc-4372-a567-0e02b2c3d479",
                EmployeeName = "Matt Smith",
                Position = "Manager",
                Department = "Sales",
                ContactNumber = "541-222-3574",
                HireDate = DateTime.Now.AddYears(-10),
                CurrentPositionStartDate = DateTime.Now.AddYears(-2),
                UserName = "mattsmith",
                NormalizedUserName = "MATTSMITH",
                Email = "matt@example.com",
                NormalizedEmail = "MATT@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var admin2 = new Employee
            {
                Id = "550e8400-e29b-41d4-a716-446655440000",
                EmployeeName = "Angelina Jones",
                Position = "Salesperson",
                Department = "Sales",
                ContactNumber = "288-433-4432",
                HireDate = DateTime.Now.AddYears(-5),
                CurrentPositionStartDate = DateTime.Now.AddYears(-5),
                UserName = "angelinaj",
                NormalizedUserName = "ANGELINAJ",
                Email = "angelina@example.com",
                NormalizedEmail = "ANGELINA@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Sales@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var admin3 = new Employee
            {
                Id = "768e2a71-58f1-4d44-9a61-1e41b0972b5a",
                EmployeeName = "Sarah Connor",
                Position = "Technician",
                Department = "Maintenance",
                ContactNumber = "647-872-9432",
                HireDate = DateTime.Now.AddYears(-3),
                CurrentPositionStartDate = DateTime.Now.AddYears(-2),
                UserName = "sarahconnor",
                NormalizedUserName = "SARAHCONNOR",
                Email = "sarah@example.com",
                NormalizedEmail = "SARAH@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Tech@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var admin4 = new Employee
            {
                Id = "dff31e3b-123a-48f9-942b-4d1b34a14e1c",
                EmployeeName = "James Roberts",
                Position = "Finance Manager",
                Department = "Finance",
                ContactNumber = "403-555-7832",
                HireDate = DateTime.Now.AddYears(-6),
                CurrentPositionStartDate = DateTime.Now.AddYears(-4),
                UserName = "jamesroberts",
                NormalizedUserName = "JAMESROBERTS",
                Email = "james@example.com",
                NormalizedEmail = "JAMES@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Finance@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            modelBuilder.Entity<Employee>().HasData(admin1, admin2, admin3, admin4);


            // PayrollRecord 시드 데이터
            modelBuilder.Entity<PayrollRecord>().HasData(

            // Matt Smith (admin1) - 2024년 6월 시작
            new PayrollRecord { PayrollRecordId = -1, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2024, 6, 1), BaseSalary = 4500, TotalBeforeTax = 4500, Tax = 405, TotalPay = 4095 },
            new PayrollRecord { PayrollRecordId = -2, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2024, 7, 1), BaseSalary = 4700, TotalBeforeTax = 4700, Tax = 423, TotalPay = 4277 },
            new PayrollRecord { PayrollRecordId = -3, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2024, 8, 1), BaseSalary = 4900, TotalBeforeTax = 4900, Tax = 441, TotalPay = 4459 },
            new PayrollRecord { PayrollRecordId = -4, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2024, 9, 1), BaseSalary = 5150, TotalBeforeTax = 5150, Tax = 464, TotalPay = 4686 },
            new PayrollRecord { PayrollRecordId = -5, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2024, 10, 1), BaseSalary = 5400, TotalBeforeTax = 5400, Tax = 486, TotalPay = 4914 },
            new PayrollRecord { PayrollRecordId = -6, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2024, 11, 1), BaseSalary = 5600, TotalBeforeTax = 5600, Tax = 504, TotalPay = 5096 },
            new PayrollRecord { PayrollRecordId = -7, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2024, 12, 1), BaseSalary = 5800, TotalBeforeTax = 5800, Tax = 522, TotalPay = 5278 },
            new PayrollRecord { PayrollRecordId = -8, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2025, 1, 1), BaseSalary = 6000, TotalBeforeTax = 6000, Tax = 540, TotalPay = 5460 },
            new PayrollRecord { PayrollRecordId = -9, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", PayDate = new DateTime(2025, 2, 1), BaseSalary = 6200, TotalBeforeTax = 6200, Tax = 558, TotalPay = 5642 },

            // Angelina Jones (admin2) - 2024년 7월 시작
            new PayrollRecord { PayrollRecordId = -10, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2024, 7, 1), BaseSalary = 5000, TotalBeforeTax = 5000, Tax = 450, TotalPay = 4550 },
            new PayrollRecord { PayrollRecordId = -11, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2024, 8, 1), BaseSalary = 5250, TotalBeforeTax = 5250, Tax = 473, TotalPay = 4777 },
            new PayrollRecord { PayrollRecordId = -12, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2024, 9, 1), BaseSalary = 5500, TotalBeforeTax = 5500, Tax = 495, TotalPay = 5005 },
            new PayrollRecord { PayrollRecordId = -13, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2024, 10, 1), BaseSalary = 5800, TotalBeforeTax = 5800, Tax = 522, TotalPay = 5278 },
            new PayrollRecord { PayrollRecordId = -14, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2024, 11, 1), BaseSalary = 6000, TotalBeforeTax = 6000, Tax = 540, TotalPay = 5460 },
            new PayrollRecord { PayrollRecordId = -15, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2024, 12, 1), BaseSalary = 6300, TotalBeforeTax = 6300, Tax = 567, TotalPay = 5733 },
            new PayrollRecord { PayrollRecordId = -16, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2025, 1, 1), BaseSalary = 6500, TotalBeforeTax = 6500, Tax = 585, TotalPay = 5915 },
            new PayrollRecord { PayrollRecordId = -17, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", PayDate = new DateTime(2025, 2, 1), BaseSalary = 6700, TotalBeforeTax = 6700, Tax = 603, TotalPay = 6097 },

            // James Roberts (admin4) - 2024년 8월 시작
            new PayrollRecord { PayrollRecordId = -24, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 8, 1), BaseSalary = 5800, TotalBeforeTax = 5800, Tax = 522, TotalPay = 5278 },
            new PayrollRecord { PayrollRecordId = -25, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 9, 1), BaseSalary = 6100, TotalBeforeTax = 6100, Tax = 549, TotalPay = 5551 },
            new PayrollRecord { PayrollRecordId = -26, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 10, 1), BaseSalary = 6300, TotalBeforeTax = 6300, Tax = 567, TotalPay = 5733 },
            new PayrollRecord { PayrollRecordId = -27, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 11, 1), BaseSalary = 6500, TotalBeforeTax = 6500, Tax = 585, TotalPay = 5915 },
            new PayrollRecord { PayrollRecordId = -28, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 12, 1), BaseSalary = 6700, TotalBeforeTax = 6700, Tax = 603, TotalPay = 6097 }
,

            // James Roberts (admin4) - 2024년 8월 시작
            new PayrollRecord { PayrollRecordId = -34, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 8, 1), BaseSalary = 5800, TotalBeforeTax = 5800, Tax = 522, TotalPay = 5278 },
            new PayrollRecord { PayrollRecordId = -35, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 9, 1), BaseSalary = 6100, TotalBeforeTax = 6100, Tax = 549, TotalPay = 5551 },
            new PayrollRecord { PayrollRecordId = -36, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 10, 1), BaseSalary = 6300, TotalBeforeTax = 6300, Tax = 567, TotalPay = 5733 },
            new PayrollRecord { PayrollRecordId = -37, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 11, 1), BaseSalary = 6500, TotalBeforeTax = 6500, Tax = 585, TotalPay = 5915 },
            new PayrollRecord { PayrollRecordId = -38, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2024, 12, 1), BaseSalary = 6700, TotalBeforeTax = 6700, Tax = 603, TotalPay = 6097 },
            new PayrollRecord { PayrollRecordId = -39, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2025, 1, 1), BaseSalary = 6900, TotalBeforeTax = 6900, Tax = 621, TotalPay = 6279 },
            new PayrollRecord { PayrollRecordId = -40, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", PayDate = new DateTime(2025, 2, 1), BaseSalary = 7100, TotalBeforeTax = 7100, Tax = 639, TotalPay = 6461 }
            );


            // SalesRecord 시드 데이터
            modelBuilder.Entity<SalesRecord>().HasData(
            // Matt Smith (admin1) - 2024년 6월 시작
            new SalesRecord { SalesRecordId = -1, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", VehicleId = 1, SaleDate = new DateTime(2024, 6, 5), SalePrice = 1200, CommissionRate = 0.05m },
            new SalesRecord { SalesRecordId = -2, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", VehicleId = 2, SaleDate = new DateTime(2024, 7, 12), SalePrice = 2500, CommissionRate = 0.05m },
            new SalesRecord { SalesRecordId = -11, EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479", VehicleId = 11, SaleDate = new DateTime(2024, 12, 25), SalePrice = 3000, CommissionRate = 0.05m },

            // Angelina Jones (admin2) - 2024년 7월 시작
            new SalesRecord { SalesRecordId = -3, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", VehicleId = 3, SaleDate = new DateTime(2024, 7, 7), SalePrice = 1000, CommissionRate = 0.05m },
            new SalesRecord { SalesRecordId = -4, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", VehicleId = 4, SaleDate = new DateTime(2024, 9, 11), SalePrice = 900, CommissionRate = 0.05m },
            new SalesRecord { SalesRecordId = -5, EmployeeId = "550e8400-e29b-41d4-a716-446655440000", VehicleId = 5, SaleDate = new DateTime(2025, 2, 5), SalePrice = 1700, CommissionRate = 0.05m },

            // Sarah Connor (admin3) - 2024년 9월 시작
            new SalesRecord { SalesRecordId = -6, EmployeeId = "768e2a71-58f1-4d44-9a61-1e41b0972b5a", VehicleId = 6, SaleDate = new DateTime(2024, 9, 10), SalePrice = 2000, CommissionRate = 0.05m },
            new SalesRecord { SalesRecordId = -7, EmployeeId = "768e2a71-58f1-4d44-9a61-1e41b0972b5a", VehicleId = 7, SaleDate = new DateTime(2024, 10, 12), SalePrice = 2550, CommissionRate = 0.05m },
            new SalesRecord { SalesRecordId = -8, EmployeeId = "768e2a71-58f1-4d44-9a61-1e41b0972b5a", VehicleId = 8, SaleDate = new DateTime(2024, 11, 6), SalePrice = 2850, CommissionRate = 0.05m },
            new SalesRecord { SalesRecordId = -9, EmployeeId = "768e2a71-58f1-4d44-9a61-1e41b0972b5a", VehicleId = 9, SaleDate = new DateTime(2025, 1, 18), SalePrice = 2570, CommissionRate = 0.05m },

            // James Roberts (admin4) - 2024년 12월 시작
            new SalesRecord { SalesRecordId = -10, EmployeeId = "dff31e3b-123a-48f9-942b-4d1b34a14e1c", VehicleId = 10, SaleDate = new DateTime(2024, 12, 19), SalePrice = 3000, CommissionRate = 0.05m }
            );




            // Customers 데이터
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "David", LastName = "Johnson", PhoneNumber = "573-486-7190", Email = "jenil.rashmi@gmail.com", Province = "Ontario", City = "Toronto", Street = "Queen St", UnitNumber = "10A", PostalCode = "M5H1B6" },
                new Customer { CustomerId = 2, FirstName = "Michael", LastName = "Jackson", PhoneNumber = "942-654-3210", Email = "michael@email.com", Province = "British Columbia", City = "Vancouver", Street = "Kingsway", UnitNumber = "5B", PostalCode = "V5R5H6" }
            );

            // Vehicles 데이터 (고객 ID 추가됨)
            
                modelBuilder.Entity<Vehicle>().HasData(
    new Vehicle
    {
        VehicleId = 1,
        Vin = "1HGCM82633A123456",
        PlateNumber = "ABC123",
        Mileage = "48000km",
        Type = "Sedan",
        Powertrain = "Gasoline",
        Model = "Accord",
        Manufacturer = "Honda",
        Year = 2022,
        MarketValue = 22000,
        Status = "Available",
        CustomerId = 1,
        ImageFileName = "2022Accord.jpg"
    },
    new Vehicle
    {
        VehicleId = 2,
        Vin = "2HGFA16548H123456",
        PlateNumber = "XYZ789",
        Mileage = "2500km",
        Type = "SUV",
        Powertrain = "Hybrid",
        Model = "CR-V",
        Manufacturer = "Honda",
        Year = 2023,
        MarketValue = 28000,
        Status = "Available",
        CustomerId = 2,
        ImageFileName = "2023CR-V.jpg"
    },
    new Vehicle
    {
        VehicleId = 3,
        Vin = "3FADP4BJ2KM123456",
        PlateNumber = "DEF456",
        Mileage = "52000km",
        Type = "Hatchback",
        Powertrain = "Gasoline",
        Model = "Focus",
        Manufacturer = "Ford",
        Year = 2016,
        MarketValue = 20000,
        Status = "Sold",
        CustomerId = 1,
        ImageFileName = "2016Focus.jpg"
    },
    new Vehicle
    {
        VehicleId = 4,
        Vin = "4T1BF1FK1HU123456",
        PlateNumber = "GHI789",
        Mileage = "33000km",
        Type = "Sedan",
        Powertrain = "Hybrid",
        Model = "Camry",
        Manufacturer = "Toyota",
        Year = 2020,
        MarketValue = 25000,
        Status = "Available",
        CustomerId = 2,
        ImageFileName = "2020Camry.jpg"
    },
    new Vehicle
    {
        VehicleId = 5,
        Vin = "5YJ3E1EA7LF123456",
        PlateNumber = "JKL012",
        Mileage = "7000km",
        Type = "Sedan",
        Powertrain = "Electric",
        Model = "Model 3",
        Manufacturer = "Tesla",
        Year = 2023,
        MarketValue = 35000,
        Status = "Available",
        CustomerId = 1,
        ImageFileName = "2023Model3.jpg"
    },
    new Vehicle
    {
        VehicleId = 6,
        Vin = "JH4KA8260NC983245",
        PlateNumber = "LMN456",
        Mileage = "59300km",
        Type = "SUV",
        Powertrain = "Gasoline",
        Model = "Explorer",
        Manufacturer = "Ford",
        Year = 2022,
        MarketValue = 26500,
        Status = "Sold",
        CustomerId = 2,
        ImageFileName = "2022Explorer.jpg"
    },
    new Vehicle
    {
        VehicleId = 7,
        Vin = "3GCUKREC1JG491275",
        PlateNumber = "QWE789",
        Mileage = "11000km",
        Type = "Truck",
        Powertrain = "Diesel",
        Model = "Silverado",
        Manufacturer = "Chevrolet",
        Year = 2023,
        MarketValue = 37000,
        Status = "Under Repair",
        CustomerId = 1,
        ImageFileName = "2023Silverado.jpg"
    },
    new Vehicle
    {
        VehicleId = 8,
        Vin = "WBXHT3C36J5F81234",
        PlateNumber = "RTY852",
        Mileage = "26400km",
        Type = "SUV",
        Powertrain = "Hybrid",
        Model = "X5",
        Manufacturer = "BMW",
        Year = 2021,
        MarketValue = 39000,
        Status = "Available",
        CustomerId = 2,
        ImageFileName = "2021X5.jpg"
    },
    new Vehicle
    {
        VehicleId = 9,
        Vin = "JN1CV6EL7MM975312",
        PlateNumber = "UIO369",
        Mileage = "42000km",
        Type = "Sedan",
        Powertrain = "Gasoline",
        Model = "Altima",
        Manufacturer = "Nissan",
        Year = 2023,
        MarketValue = 19500,
        Status = "Available",
        CustomerId = 1,
        ImageFileName = "2023Altima.jpg"
    },
    new Vehicle
    {
        VehicleId = 10,
        Vin = "KMHDH4AE3EU653948",
        PlateNumber = "PAS741",
        Mileage = "1000km",
        Type = "Sedan",
        Powertrain = "Hybrid",
        Model = "Sonata",
        Manufacturer = "Hyundai",
        Year = 2024,
        MarketValue = 22000,
        Status = "Available",
        CustomerId = 2,
        ImageFileName = "2024Sonata.jpg"
    },
    new Vehicle
    {
        VehicleId = 11,
        Vin = "KNDPM3AC2H7128394",
        PlateNumber = "GHJ654",
        Mileage = "2000km",
        Type = "SUV",
        Powertrain = "Electric",
        Model = "Sportage",
        Manufacturer = "Kia",
        Year = 2022,
        MarketValue = 28500,
        Status = "Available",
        CustomerId = 1,
        ImageFileName = "2022Sportage.jpg"
    }
);


            // Vehicle Maintenance 시드 데이터 추가
            modelBuilder.Entity<VehicleMaintenance>().HasData(
                new VehicleMaintenance
                {
                    VehicleMaintenanceId = 1,
                    VehicleId = 1,
                    MaintenanceType = "Oil Change",
                    LastServiceDate = DateTime.Now.AddYears(-1),
                    Cost = 50,
                    TaxAmount = 5,
                    TotalAmount = 55,
                    MaintenanceDescription = "Routine oil change"
                },
                new VehicleMaintenance
                {
                    VehicleMaintenanceId = 2,
                    VehicleId = 1,
                    MaintenanceType = "Brake Pad Replacement",
                    LastServiceDate = DateTime.Now.AddYears(-2),
                    Cost = 200,
                    TaxAmount = 20,
                    TotalAmount = 220,
                    MaintenanceDescription = "Replaced worn-out brake pads"
                },
                new VehicleMaintenance
                {
                    VehicleMaintenanceId = 3,
                    VehicleId = 2,
                    MaintenanceType = "Tire Rotation",
                    LastServiceDate = DateTime.Now.AddDays(-45),
                    Cost = 40,
                    TaxAmount = 4,
                    TotalAmount = 44,
                    MaintenanceDescription = "Routine tire rotation for even wear"
                },
                new VehicleMaintenance
                {
                    VehicleMaintenanceId = 4,
                    VehicleId = 2,
                    MaintenanceType = "Battery Replacement",
                    LastServiceDate = DateTime.Now.AddYears(-5),
                    Cost = 150,
                    TaxAmount = 15,
                    TotalAmount = 165,
                    MaintenanceDescription = "Replaced old car battery"
                },
                new VehicleMaintenance
                {
                    VehicleMaintenanceId = 5,
                    VehicleId = 1,
                    MaintenanceType = "Engine Tune-Up",
                    LastServiceDate = DateTime.Now.AddDays(-90),
                    Cost = 300,
                    TaxAmount = 30,
                    TotalAmount = 330,
                    MaintenanceDescription = "Complete engine diagnostic and tune-up"
                }
            );



            modelBuilder.Entity<VehicleTransaction>().HasData(
            new VehicleTransaction
            {
                VehicleTransactionId = 1,
                TransactionType = "Sell",
                TransactionDate = new DateTime(2024, 9, 1),
                PurchasePrice = 20000,
                SalesPrice = 26000,
                TradeInValue = 3000,
                FinalPrice = 26000,
                MarginRate = 5.5m,
                VehicleId = 1, 
                EmployeeId = "f47ac10b-58cc-4372-a567-0e02b2c3d479"
            },
            new VehicleTransaction
            {
                VehicleTransactionId = 2,
                TransactionType = "Buy",
                TransactionDate = new DateTime(2024, 12, 15),
                PurchasePrice = 18000,
                SalesPrice = 22500,
                TradeInValue = 2500,
                FinalPrice = 22500,
                MarginRate = 4.8m,
                VehicleId = 2,
                EmployeeId = "768e2a71-58f1-4d44-9a61-1e41b0972b5a"
            },
            new VehicleTransaction
            {
                VehicleTransactionId = 3,
                TransactionType = "TradeIn",
                TransactionDate = new DateTime(2024, 1, 20),
                PurchasePrice = 28000,
                SalesPrice = 36000,
                TradeInValue = 5000,
                FinalPrice = 36000,
                MarginRate = 6.2m,
                VehicleId = 4,
                EmployeeId = "550e8400-e29b-41d4-a716-446655440000"
            },
            new VehicleTransaction
            {
                VehicleTransactionId = 4,
                TransactionType = "Sell",
                TransactionDate = new DateTime(2024, 2, 28),
                PurchasePrice = 47000,
                SalesPrice = 56000,
                TradeInValue = 6000,
                FinalPrice = 56000,
                MarginRate = 5.8m,
                VehicleId = 3,
                EmployeeId = "768e2a71-58f1-4d44-9a61-1e41b0972b5a"
            }
        );





            // Appointments 데이터
            modelBuilder.Entity<Appointment>().HasData(
        new Appointment { AppointmentId = 1, CustomerId = 1, VehicleId = 1, EmployeeId = admin1.Id, AppointmentDate = DateTime.Now.AddDays(-5), Status = "Completed", AppointmentType = "Service", Description = "Oil Change" }
    );

            // SalesRecord 데이터
            modelBuilder.Entity<SalesRecord>().HasData(
                new SalesRecord { SalesRecordId = 1, VehicleId = 1, EmployeeId = admin1.Id, SaleDate = DateTime.Now.AddDays(-30), SalePrice = 22000, CommissionEarned = 1100, CommissionRate = 0.05m }
            );

            // PayrollRecord 데이터
            modelBuilder.Entity<PayrollRecord>().HasData(
                new PayrollRecord { PayrollRecordId = 1, EmployeeId = admin1.Id, PayDate = DateTime.Now.AddMonths(-1), BaseSalary = 5000, TotalBeforeTax = 5500, Tax = 500, TotalPay = 5000 }
            );
        }
    }
}
