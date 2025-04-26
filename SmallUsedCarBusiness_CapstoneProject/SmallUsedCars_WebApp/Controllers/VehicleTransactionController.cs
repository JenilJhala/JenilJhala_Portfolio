using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Models;
using SmallUsedCars_WebApp.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    public class VehicleTransactionController : Controller
    {
        private readonly SalesService _salesService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public VehicleTransactionController(SalesService salesService, ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _salesService = salesService;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Vehicles
                .GroupJoin(
                    _context.VehicleTransactions,
                    v => v.VehicleId,
                    t => t.VehicleId,
                    (v, transactions) => new
                    {
                        Vehicle = v,
                        LatestTransaction = transactions.OrderByDescending(t => t.TransactionDate).FirstOrDefault()
                    })
                .Select(vt => new VehicleInventoryViewModel
                {
                    VehicleId = vt.Vehicle.VehicleId,
                    Manufacturer = vt.Vehicle.Manufacturer,
                    Mileage = vt.Vehicle.Mileage,
                    Model = vt.Vehicle.Model,
                    PlateNumber = vt.Vehicle.PlateNumber,
                    Status = vt.Vehicle.Status,
                    MarketValue = vt.Vehicle.MarketValue,
                    TransactionType = vt.LatestTransaction != null ? vt.LatestTransaction.TransactionType : "Pending"
                })
                .ToListAsync();

            return View(vehicles);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerBuyVehicle(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null) return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            // 사용자가 입력할 필요 없는 정보는 DB에서 조회하도록 뷰모델에 포함하지 않음
            var model = new transactionInfoViewModel
            {
                VehicleId = vehicle.VehicleId,
                EmployeeId = currentUser.Id, // 로그인한 직원 자동 설정
                TransactionDate = DateTime.Today
            };

            return View("CustomerBuyVehicle", model);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerBuyVehicle(transactionInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerBuyVehicle", model);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = "Unauthorized user.";
                return Unauthorized();
            }

            // 차량 정보를 DB에서 가져옴
            var vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
            if (vehicle == null)
            {
                TempData["ErrorMessage"] = "Vehicle not found.";
                return NotFound();
            }

            if (vehicle.Status == "Sold")
            {
                TempData["ErrorMessage"] = "This vehicle has already been sold.";
                return RedirectToAction("List", "Inventory");
            }

            try
            {
                // 차량 상태를 "Sold"로 변경 후 저장
                vehicle.Status = "Sold";
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                var transaction = new VehicleTransaction
                {
                    VehicleId = vehicle.VehicleId,
                    TransactionType = "Sell",
                    TransactionDate = model.TransactionDate,
                    PurchasePrice = model.PurchasePrice,
                    SalesPrice = model.SalesPrice,
                    TradeInValue = model.TradeInValue,
                    FinalPrice = model.SalesPrice - model.TradeInValue,
                    MarginRate = model.PurchasePrice > 0 ? ((model.SalesPrice - model.PurchasePrice) / model.PurchasePrice) * 100 : 0,
                    EmployeeId = currentUser.Id
                };

                _context.VehicleTransactions.Add(transaction);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Transaction recorded successfully.";
                return RedirectToAction("List", "Inventory");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("List", "Inventory");
            }
        }

        //  딜러쉽이 차량을 구매하는 경우 (GET)
        [HttpGet]
        public async Task<IActionResult> SellVehicleToDealership(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null) return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            var model = new transactionInfoViewModel
            {
                VehicleId = vehicle.VehicleId,
                TransactionDate = DateTime.Today
            };

            return View("SellVehicleToDealership", model);
        }

        //  딜러쉽이 차량을 구매하는 경우 (POST)
        [HttpPost]
        public async Task<IActionResult> SellVehicleToDealership(transactionInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("SellVehicleToDealership", model);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            var vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
            if (vehicle == null) return NotFound();

            try
            {
                vehicle.Status = "Available";
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                var transaction = new VehicleTransaction
                {
                    VehicleId = vehicle.VehicleId,
                    TransactionType = "Buy",
                    TransactionDate = model.TransactionDate,
                    PurchasePrice = model.PurchasePrice > 0 ? model.PurchasePrice : 0,
                    SalesPrice = model.SalesPrice > 0 ? model.SalesPrice : 0,
                    TradeInValue = model.TradeInValue > 0 ? model.TradeInValue : 0,
                    FinalPrice = model.SalesPrice - model.TradeInValue,
                    MarginRate = model.PurchasePrice > 0 ? ((model.SalesPrice - model.PurchasePrice) / model.PurchasePrice) * 100 : 0,
                    EmployeeId = currentUser.Id
                };

                _context.VehicleTransactions.Add(transaction);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Transaction recorded successfully.";
                return RedirectToAction("List", "Inventory");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("List", "Inventory");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EnterTransactionInfo(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null) return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            var model = new transactionInfoViewModel
            {
                VehicleId = vehicle.VehicleId,
                TransactionDate = DateTime.Now
            };

            return View("EnterTransactionInfo", model);
        }

        [HttpPost]
        public async Task<IActionResult> EnterTransactionInfo(transactionInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EnterTransactionInfo", model);
            }

            var vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
            if (vehicle == null)
            {
                ModelState.AddModelError("", "Vehicle not found.");
                return View("EnterTransactionInfo", model);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Unauthorized user.");
                return View("EnterTransactionInfo", model);
            }

            try
            {
                // 차량 상태 변경
                vehicle.Status = "Available";
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                // 새로운 거래 기록 저장
                var transaction = new VehicleTransaction
                {
                    VehicleId = vehicle.VehicleId,
                    TransactionType = "Buy",
                    TransactionDate = model.TransactionDate,
                    PurchasePrice = model.PurchasePrice,
                    SalesPrice = model.SalesPrice,
                    TradeInValue = model.TradeInValue,
                    FinalPrice = model.SalesPrice - model.TradeInValue,
                    MarginRate = model.PurchasePrice > 0 ? ((model.SalesPrice - model.PurchasePrice) / model.PurchasePrice) * 100 : 0,
                    EmployeeId = currentUser.Id  // 로그인한 직원 ID 자동 할당
                };

                _context.VehicleTransactions.Add(transaction);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Transaction recorded successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("EnterTransactionInfo", model);
            }
        }
    }
}
