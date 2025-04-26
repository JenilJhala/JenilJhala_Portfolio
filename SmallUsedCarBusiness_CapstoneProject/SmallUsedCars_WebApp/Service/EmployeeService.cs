using Microsoft.AspNetCore.Identity;
using SmallUsedCars_WebApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Service
{
    public class EmployeeService
    {
        private readonly UserManager<Employee> _userManager; // Identity의 UserManager 사용

        public EmployeeService(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        // 직원 정보 가져오기 (Identity 기반)
        public List<Employee> GetAllEmployees()
        {
            var employees = _userManager.Users
                .Where(u => !string.IsNullOrEmpty(u.EmployeeName)) // 직원 이름이 NULL이 아닌 경우만
                .ToList();

            return employees ?? new List<Employee>();
        }

        // Retrieve a single employee by their Identity Id
        public Employee GetEmployeeById(string employeeId)
        {
            return _userManager.Users.FirstOrDefault(e => e.Id == employeeId);
        }
    }
}
