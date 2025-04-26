using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using System;

namespace SmallUsedCars_WebApp.Service
{
    public class SalesService
    {
        private readonly ApplicationDbContext _context;

        //  PayrollService를 제거하여 순환 의존성 해결
        // SalesService는 PayrollService를 참조하지 않도록 변경했습니다.
        // PayrollService는 SalesRecords에서 커미션을 직접 조회하도록 변경했습니다.
        public SalesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SellVehicleToDealership(string employeeId, Vehicle purchasedVehicle, decimal purchasePrice, List<VehicleTransaction> purchaseRecords)
        {
            // Commission rate for dealership purchases (0.2%)
            decimal commissionRate = 0.002m;
            decimal commission = purchasePrice * commissionRate;

            // Create a new transaction record for purchasing a vehicle from a customer
            var transaction = new VehicleTransaction
            {
                TransactionDate = DateTime.Now,
                TransactionType = "Dealership Purchase",
                PurchasePrice = purchasePrice,
                FinalPrice = purchasePrice,
                MarginRate = 0m,
                EmployeeId = employeeId
            };

            _context.VehicleTransactions.Add(transaction);
            _context.Inventories.Add(new Inventory { Vehicle = purchasedVehicle });

            foreach (var record in purchaseRecords)
            {
                _context.VehicleTransactions.Add(record);
            }

            //  커미션을 SalesRecords에 저장 (PayrollService에서 이 데이터를 활용)
            var saleRecord = new SalesRecord
            {
                EmployeeId = employeeId,
                VehicleId = purchasedVehicle.VehicleId,
                SaleDate = DateTime.Now,
                SalePrice = purchasePrice,
                CommissionEarned = commission,
                CommissionRate = commissionRate
            };

            _context.SalesRecords.Add(saleRecord);
            _context.SaveChanges();
        }

        public void TradeInVehicle(string employeeId, Vehicle newVehicle, decimal tradeInValue, decimal purchasePrice, List<Inventory> inventory, List<VehicleTransaction> purchaseRecords)
        {
            decimal commissionRate = 0.005m;
            decimal finalPrice = purchasePrice - tradeInValue;
            decimal marginRate = finalPrice / purchasePrice;
            decimal commission = finalPrice * commissionRate;

            var transaction = new VehicleTransaction
            {
                TransactionDate = DateTime.Now,
                TransactionType = "Trade-In",
                PurchasePrice = purchasePrice,
                FinalPrice = finalPrice,
                MarginRate = marginRate,
                EmployeeId = employeeId
            };

            _context.VehicleTransactions.Add(transaction);
            _context.Vehicles.Add(newVehicle);

            foreach (var item in inventory)
            {
                _context.Inventories.Add(item);
            }

            foreach (var record in purchaseRecords)
            {
                _context.VehicleTransactions.Add(record);
            }

            var saleRecord = new SalesRecord
            {
                EmployeeId = employeeId,
                VehicleId = newVehicle.VehicleId,
                SaleDate = DateTime.Now,
                SalePrice = finalPrice,
                CommissionEarned = commission,
                CommissionRate = commissionRate
            };

            _context.SalesRecords.Add(saleRecord);
            _context.SaveChanges();
        }

        public void CustomerBuyVehicle(string employeeId, Vehicle soldVehicle, decimal salePrice, List<Inventory> inventory)
        {
            decimal commissionRate = 0.005m;
            decimal finalPrice = salePrice * 1.1m;
            decimal marginRate = 0.1m;
            decimal commission = salePrice * commissionRate;

            var transaction = new VehicleTransaction
            {
                TransactionDate = DateTime.Now,
                TransactionType = "Customer Purchase",
                SalesPrice = salePrice,
                FinalPrice = finalPrice,
                MarginRate = marginRate,
                EmployeeId = employeeId
            };

            _context.VehicleTransactions.Add(transaction);
            soldVehicle.Status = "Sold";
            _context.Vehicles.Update(soldVehicle);

            foreach (var item in inventory)
            {
                _context.Inventories.Remove(item);
            }

            var saleRecord = new SalesRecord
            {
                EmployeeId = employeeId,
                VehicleId = soldVehicle.VehicleId,
                SaleDate = DateTime.Now,
                SalePrice = salePrice,
                CommissionEarned = commission,
                CommissionRate = commissionRate
            };

            _context.SalesRecords.Add(saleRecord);
            _context.SaveChanges();
        }

        public decimal GetTotalSalesForPeriod(string employeeId, DateTime startDate, DateTime endDate)
        {
            return _context.SalesRecords
                .Where(s => s.EmployeeId == employeeId && s.SaleDate >= startDate && s.SaleDate <= endDate)
                .Sum(s => s.SalePrice);
        }

        public decimal GetTotalCommissionForPeriod(string employeeId, DateTime startDate, DateTime endDate)
        {
            return _context.SalesRecords
                .Where(s => s.EmployeeId == employeeId && s.SaleDate >= startDate && s.SaleDate <= endDate)
                .Sum(s => s.CommissionEarned);
        }
    }
}
