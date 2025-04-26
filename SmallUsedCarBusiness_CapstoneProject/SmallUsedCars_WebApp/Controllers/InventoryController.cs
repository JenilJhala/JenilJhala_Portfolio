using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Models;
using SmallUsedCars_WebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    [Route("Inventory")]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly VehicleService _vehicleService;

        public InventoryController(ApplicationDbContext context, VehicleService vehicleService)
        {
            _context = context;
            _vehicleService = vehicleService;
        }

        //  차량 목록 조회
        [HttpGet("List")]
        public IActionResult List(bool? showAddVehicleForm)
        {
            // Dealership Purchase 버튼을 눌렀을 때 Add Vehicle 폼이 보이도록 설정
            if (showAddVehicleForm.HasValue && showAddVehicleForm.Value)
            {
                TempData["ShowAddVehicleForm"] = true;
            }

            var vehicles = _context.Vehicles
                .GroupJoin(   // Vehicles 와 VehicleTransactions 를 조인
                    _context.VehicleTransactions,
                    v => v.VehicleId,  // Vehicles의 VehicleId (PK)
                    t => t.VehicleId,  // VehicleTransactions의 VehicleId (FK)
                    (v, transactions) => new
                    {
                        Vehicle = v,
                        LatestTransaction = transactions.OrderByDescending(t => t.TransactionDate).FirstOrDefault() // 가장 최근 거래 정보 가져오기
                    })
                .Select(vt => new VehicleInventoryViewModel   // Select()를 사용하여 VehicleInventoryViewModel로 변환
                {
                    VehicleId = vt.Vehicle.VehicleId,
                    Manufacturer = vt.Vehicle.Manufacturer,
                    Model = vt.Vehicle.Model,
                    PlateNumber = vt.Vehicle.PlateNumber,
                    Mileage = vt.Vehicle.Mileage,
                    Status = vt.Vehicle.Status,
                    MarketValue = vt.Vehicle.MarketValue,
                    TransactionType = vt.LatestTransaction != null ? vt.LatestTransaction.TransactionType : "Pending",
                    ImageFileName = vt.Vehicle.ImageFileName
                })
                .ToList();

            return View(vehicles);
        }

        //  차량 추가 (TempData에 VehicleId 저장 후 VehicleTransaction/Index로 리디렉트)
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Vehicle newVehicle)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid input. Please check the fields and try again.";
                return View("List", _vehicleService.GetAllVehicles());
            }

            try
            {
                // 차량을 DB에 추가
                _vehicleService.AddVehicle(newVehicle);
                await _context.SaveChangesAsync();

                // 🚀 방금 추가한 차량의 VehicleId 저장
                TempData["NewVehicleId"] = newVehicle.VehicleId;

                TempData["SuccessMessage"] = "Vehicle added successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error adding vehicle: {ex.Message}";
            }

            return RedirectToAction("Index", "VehicleTransaction");
        }

        //  차량 정보 수정
        [HttpGet("Edit/{vehicleId}")]
        public IActionResult Edit(int vehicleId)
        {
            var vehicle = _vehicleService.GetVehicleById(vehicleId);
            if (vehicle == null)
            {
                TempData["ErrorMessage"] = "Vehicle not found.";
                return RedirectToAction("List");
            }

            var editViewModel = new VehicleEditViewModel
            {
                VehicleId = vehicle.VehicleId,
                PlateNumber = vehicle.PlateNumber,
                Status = vehicle.Status,
                MarketValue = vehicle.MarketValue
            };

            return View(editViewModel);
        }

        [HttpPost("Edit/{vehicleId}")]
        public IActionResult Edit(int vehicleId, VehicleEditViewModel updatedVehicle)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedVehicle);
            }

            var existingVehicle = _vehicleService.GetVehicleById(vehicleId);
            if (existingVehicle == null)
            {
                TempData["ErrorMessage"] = "Vehicle not found.";
                return RedirectToAction("List");
            }

            try
            {
                existingVehicle.PlateNumber = updatedVehicle.PlateNumber;
                existingVehicle.Status = updatedVehicle.Status;
                existingVehicle.MarketValue = updatedVehicle.MarketValue;

                _vehicleService.UpdateVehicle(vehicleId, existingVehicle);
                TempData["SuccessMessage"] = "Vehicle updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating vehicle: {ex.Message}";
            }

            return RedirectToAction("List");
        }

        //  차량 삭제
        public IActionResult Delete(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                _vehicleService.DeleteVehicle(id);
                TempData["SuccessMessage"] = "Vehicle deleted successfully!";
            }
            catch (InvalidOperationException ex) // 예약이 설정된 차량 삭제 불가 예외 처리
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting vehicle: {ex.Message}";
            }

            return RedirectToAction("List");
        }
    }
}







/*

1. LINQ란? (전체 개념)
LINQ (Language Integrated Query)란?

C#과 같은 .NET 언어에서 컬렉션(List, Array), 데이터베이스(DB), XML 등의 데이터를 쿼리할 수 있도록 제공하는 기능
SQL과 비슷하지만, C# 코드 내에서 직접 사용 가능

2. LINQ의 주요 기능:
1 데이터 필터링 (Where) → 특정 조건을 만족하는 데이터 선택
2️ 데이터 정렬 (OrderBy, OrderByDescending) → 데이터를 정렬
3️ 데이터 변환 (Select, SelectMany) → 특정 형태로 변환
4️ 데이터 그룹화 (GroupBy) → 특정 키를 기준으로 그룹화
5️ 데이터 조인 (Join, GroupJoin) → 두 개 이상의 데이터 소스 연결
6️ 집계 함수 (Count, Sum, Average) → 데이터 개수, 합계, 평균 계산


LINQ가 강력한데, 왜 굳이 GetAll() 같은 별도의 메서드를 만들어 사용하는가?
항상 LINQ만 사용하면 안 되는가?

LINQ는 강력하지만, 코드의 유지보수성, 재사용성, 성능 최적화, 비즈니스 로직의 분리 등을 고려할 때 GetAll() 같은 별도의 메서드를 만들어 사용하는 것이 더 좋은 경우가 많음.
즉, LINQ가 필요할 때 직접 사용하고, 재사용이 필요한 로직은 별도의 함수(GetAll())로 분리하는 것이 좋음.


LINQ를 직접 사용하면 장점이 많음:

유연성: 다양한 필터링(Where()), 정렬(OrderBy()), 그룹화(GroupBy()) 등을 즉석에서 조합 가능
간결한 코드: SQL처럼 데이터를 조작할 수 있어 가독성이 좋음
즉시 실행 가능: ToList(), FirstOrDefault() 등을 사용하면 데이터를 바로 가져올 수 있음



그러나 LINQ만을 사용하면 몇 가지 단점이 있음:

코드 중복 문제:
여러 곳에서 같은 LINQ 쿼리를 사용하면 코드가 중복됨
GetAllVehicles()을 사용하면 중복된 코드 없이 여러 곳에서 재사용 가능


유지보수 어려움:
LINQ를 컨트롤러나 서비스에서 직접 쓰면 코드가 길어지고 유지보수가 어려움
GetAllVehicles() 같은 함수로 분리하면 한 곳에서 수정하면 모든 곳에서 반영 가능

성능 문제:
LINQ는 IQueryable을 사용하여 데이터베이스에 쿼리를 보낼 수 있지만, 잘못 사용하면 불필요한 데이터 로딩 발생
GetAllVehicles()에서 필요한 필드만 가져오도록 하면 성능 최적화 가능


비즈니스 로직과 데이터 로직 분리:
GetAllVehicles() 같은 함수는 서비스 계층(Service Layer)에서 비즈니스 로직을 처리
컨트롤러에서는 데이터를 어떻게 가져오는지 신경 쓰지 않고, 비즈니스 로직에 집중 가능



LINQ를 직접 사용하면 좋은 경우:
✅ 특정 컨트롤러에서 한 번만 사용되는 복잡한 쿼리
✅ 성능 최적화를 위해 필요한 필드만 가져와야 할 때
✅ 즉석에서 조건을 추가하거나 동적으로 데이터 필터링해야 할 때

GetAll() 같은 함수를 사용하면 좋은 경우:
✅ 여러 곳에서 같은 데이터 조회 로직이 필요할 때
✅ 비즈니스 로직과 데이터 접근을 분리해야 할 때
✅ 기본적으로 자주 호출되는 데이터(예: 차량 목록, 사용자 목록 등)를 제공할 때

 */