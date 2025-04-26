using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallUsedCars_WebApp.Models;
using SmallUsedCars_WebApp.Service;
using SmallUsedCars_WebApp.ViewModels;
using System;
using System.Linq;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ReportService _reportService;
        private readonly EmployeeService _employeeService;

        public ReportsController(ReportService reportService, EmployeeService employeeService)
        {
            _reportService = reportService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult List(string[] SelectedEmployeeIds, DateTime? SalesStartDate, DateTime? SalesEndDate, DateTime? CommissionStartDate, DateTime? CommissionEndDate)
        {
            var model = new ReportViewModel();
            try
            {
                // 직원 목록 가져오기
                model.Employees = _employeeService.GetAllEmployees()
                    .Select(e => new EmployeeViewModel
                    {
                        EmployeeId = e.Id,
                        EmployeeName = e.EmployeeName
                    }).ToList();

                // 선택한 직원 유지
                model.SelectedEmployeeIds = SelectedEmployeeIds?.ToList() ?? new List<string>();

                // 선택한 기간 유지
                model.SalesStartDate = SalesStartDate;
                model.SalesEndDate = SalesEndDate;
                model.CommissionStartDate = CommissionStartDate;
                model.CommissionEndDate = CommissionEndDate;

                // 매출 데이터 조회
                if (SalesStartDate.HasValue && SalesEndDate.HasValue)
                {
                    model.SalesRecords = _reportService.GetSalesByPeriod(
                        SelectedEmployeeIds.ToList(),
                        SalesStartDate.Value,
                        SalesEndDate.Value
                    );
                }

                // 커미션 데이터 조회
                if (CommissionStartDate.HasValue && CommissionEndDate.HasValue)
                {
                    model.CommissionRecords = _reportService.GetCommissionByPeriod(
                        SelectedEmployeeIds.ToList(),
                        CommissionStartDate.Value,
                        CommissionEndDate.Value
                    );
                }
            }
            catch (Exception ex)
            {
                // 에러 메시지를 모델에 저장하여 뷰에서 표시
                model.ErrorMessage = ex.Message;
            }

            return View(model);
        }

        

        [HttpGet]
        public IActionResult SalesRanking(DateTime startDate, DateTime endDate)
        {
            var ranking = _reportService.GetSalesRanking(startDate, endDate);

            ViewData["StartDate"] = startDate.ToString("yyyy-MM");
            ViewData["EndDate"] = endDate.ToString("yyyy-MM");

            return View("Ranking", ranking);
        }


    }
}
