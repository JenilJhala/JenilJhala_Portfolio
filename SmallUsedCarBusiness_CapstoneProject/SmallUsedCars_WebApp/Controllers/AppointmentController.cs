using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Service;
using SmallUsedCars_WebApp.ViewModels;
using System.Linq;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _appointmentService;
        private readonly CustomerService _customerService;
        private readonly VehicleService _vehicleService;
        private readonly UserManager<Employee> _userManager;

        public AppointmentController(
            AppointmentService appointmentService,
            CustomerService customerService,
            VehicleService vehicleService,
            UserManager<Employee> userManager)
        {
            _appointmentService = appointmentService;
            _customerService = customerService;
            _vehicleService = vehicleService;
            _userManager = userManager;
        }

        public IActionResult List()
        {
            var appointments = _appointmentService.GetAllAppointments()
                .Select(a => new AppointmentViewModel
                {
                    AppointmentId = a.AppointmentId,
                    CustomerId = a.CustomerId,
                    FirstName = a.Customer.FirstName,  
                    LastName = a.Customer.LastName,   
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee?.EmployeeName,
                    VehicleId = a.VehicleId,
                    VehicleModel = a.Vehicle?.Model,
                    VehiclePlateNumber = a.Vehicle?.PlateNumber,
                    Status = a.Status,
                    AppointmentType = a.AppointmentType,
                    AppointmentDate = a.AppointmentDate,
                    Description = a.Description
                }).ToList();

            return View(appointments); //  뷰모델 리스트를 전달
        }


        public IActionResult GetAppointmentsByCustomer(int customerId)
        {
            var appointments = _appointmentService.GetAppointmentByCustomer(customerId);
            if (appointments == null || !appointments.Any())
            {
                return NotFound();
            }
            return View("List", appointments);
        }

        public IActionResult Create()
        {
            var viewModel = new AppointmentViewModel
            {
                Customers = _customerService.GetAllCustomers(),
                Employees = _userManager.Users.ToList(),
                Vehicles = _vehicleService.GetAllVehicles()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AppointmentViewModel viewModel)
        {
            // 기존 고객을 선택하지 않았다면 새 고객 등록을 위한 검증 제거
            if (viewModel.IsNewCustomer)
            {
                ModelState.Remove(nameof(viewModel.CustomerId)); // 기존 고객 ID 제거
            }
            else // 기존 고객을 선택한 경우, 새 고객 필드의 검증 제거
            {
                ModelState.Remove(nameof(viewModel.FirstName));
                ModelState.Remove(nameof(viewModel.LastName));
                ModelState.Remove(nameof(viewModel.PhoneNumber));
                ModelState.Remove(nameof(viewModel.Email));
                ModelState.Remove(nameof(viewModel.Province));
                ModelState.Remove(nameof(viewModel.City));
                ModelState.Remove(nameof(viewModel.Street));
                ModelState.Remove(nameof(viewModel.UnitNumber));
                ModelState.Remove(nameof(viewModel.PostalCode));
            }

            // 모델 상태 검증
            if (!ModelState.IsValid)
            {
                viewModel.Customers = _customerService.GetAllCustomers();
                viewModel.Employees = _userManager.Users.ToList();
                viewModel.Vehicles = _vehicleService.GetAllVehicles();
                return View(viewModel);
            }

            // 새 고객을 추가해야 하는 경우
            if (viewModel.IsNewCustomer)
            {
                var newCustomer = new Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PhoneNumber = viewModel.PhoneNumber,
                    Email = viewModel.Email,
                    Province = viewModel.Province,
                    City = viewModel.City,
                    Street = viewModel.Street,
                    UnitNumber = string.IsNullOrWhiteSpace(viewModel.UnitNumber) ? null : viewModel.UnitNumber,
                    PostalCode = viewModel.PostalCode
                };

                // 고객 생성 및 CustomerId 설정
                newCustomer = _customerService.CreateCustomer(newCustomer);
                viewModel.CustomerId = newCustomer.CustomerId;
            }

            // 고객 ID가 여전히 0이라면 오류 처리
            if (viewModel.CustomerId == 0)
            {
                ModelState.AddModelError("CustomerId", "Customer selection or entry is required.");
                viewModel.Customers = _customerService.GetAllCustomers();
                viewModel.Employees = _userManager.Users.ToList();
                viewModel.Vehicles = _vehicleService.GetAllVehicles();
                return View(viewModel);
            }

            // 예약 생성
            _appointmentService.CreateAppointment(viewModel);
            return RedirectToAction("List");
        }


        public IActionResult Update(int id)
        {
            var appointment = _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateAppointmentViewModel
            {
                AppointmentId = appointment.AppointmentId,
                CustomerId = appointment.CustomerId,
                CustomerName = $"{appointment.Customer.FirstName} {appointment.Customer.LastName}",
                Customers = _customerService.GetAllCustomers(),
                EmployeeId = appointment.EmployeeId,
                Employees = _userManager.Users.ToList(),
                VehicleId = appointment.VehicleId,
                Vehicles = _vehicleService.GetAllVehicles(),
                Status = appointment.Status,
                AppointmentType = appointment.AppointmentType,
                AppointmentDate = appointment.AppointmentDate,
                Description = appointment.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(UpdateAppointmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // 데이터가 유효하지 않으면 다시 ViewModel 데이터를 설정하고 뷰로 반환
                viewModel.Customers = _customerService.GetAllCustomers();
                viewModel.Employees = _userManager.Users.ToList();
                viewModel.Vehicles = _vehicleService.GetAllVehicles();
                return View(viewModel);
            }

            var appointment = new Appointment
            {
                AppointmentId = viewModel.AppointmentId,
                CustomerId = viewModel.CustomerId,
                EmployeeId = viewModel.EmployeeId,
                VehicleId = viewModel.VehicleId,
                Status = viewModel.Status,
                AppointmentType = viewModel.AppointmentType,
                AppointmentDate = viewModel.AppointmentDate,
                Description = viewModel.Description
            };

            _appointmentService.UpdateAppointment(appointment);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var appointment = _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment); // Delete.cshtml 페이지로 이동
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var appointment = _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _appointmentService.DeleteAppointment(id);
            return RedirectToAction("List");
        }


    }
}
