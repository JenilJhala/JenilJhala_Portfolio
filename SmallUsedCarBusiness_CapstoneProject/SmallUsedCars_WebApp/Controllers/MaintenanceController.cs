using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            var today = DateTime.Now;

            // VehicleMaintenance 데이터를 가져와서 ViewModel에 매핑
            var maintenanceRecords = _context.MaintenanceRecords
                .Include(m => m.Vehicle)
                .ThenInclude(v => v.Customer)
                .Select(m => new VehicleMaintenanceViewModel
                {
                    VehicleId = m.VehicleId,
                    VehicleModel = m.Vehicle.Model,
                    CustomerEmail = m.Vehicle.Customer != null ? m.Vehicle.Customer.Email : "N/A",
                    MaintenanceType = m.MaintenanceType,
                    LastServiceDate = m.LastServiceDate,
                    Cost = $"${m.TotalAmount}",
                    MaintenanceDescription = m.MaintenanceDescription,

                    // 유지보수 만기 도래 여부 판단
                    IsDueForReplacement = (
                        (m.MaintenanceType == "Battery Replacement" && m.LastServiceDate.AddYears(5) <= today) ||
                        (m.MaintenanceType == "Oil Change" && m.LastServiceDate.AddYears(1) <= today) ||
                        (m.MaintenanceType == "Brake Pad Replacement" && m.LastServiceDate.AddYears(2) <= today)
                    ),
                    CustomerId = m.Vehicle.Customer != null ? m.Vehicle.Customer.CustomerId : (int?)null
                })
                .ToList();

            return View(maintenanceRecords);
        }
    }
}
