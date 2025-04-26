using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Service
{
    public class VehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }


        // GetAllVehicles()는 단순히 차량 목록만 반환하므로 거래 기록이 포함되지 않음
        // 

        public List<Vehicle> GetAllVehicles()
        {
            return _context.Vehicles.ToList() ?? new List<Vehicle>();
        }



        public Vehicle GetVehicleById(int vehicleId)
        {
            return _context.Vehicles.Find(vehicleId);
        }



        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }



        // PlateNumber, Status, MarketValue만 업데이트 가능하게 제한(컨트롤러의 edit액션, 뷰의 입력필드도 수정 요)
        public void UpdateVehicle(int vehicleId, Vehicle updatedVehicle)
        {
            var vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                vehicle.PlateNumber = updatedVehicle.PlateNumber;
                vehicle.Status = updatedVehicle.Status;
                vehicle.MarketValue = updatedVehicle.MarketValue;
                _context.SaveChanges();
            }
        }

        public void DeleteVehicle(int vehicleId)
        {
            // 차량이 Appointments 테이블에 예약되어 있는지 확인
            bool hasAppointments = _context.Appointments.Any(a => a.VehicleId == vehicleId);

            if (hasAppointments)
            {
                throw new InvalidOperationException("This vehicle cannot be deleted because it has scheduled appointments");
            }

            var vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }
    }
}
