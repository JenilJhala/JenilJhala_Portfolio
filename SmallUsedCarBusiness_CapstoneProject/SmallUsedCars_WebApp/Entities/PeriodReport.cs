using SmallUsedCars_WebApp.Entities;

namespace SmallUsedCars_WebApp.Models
{
    public class PeriodReport
    {
        public string Period { get; set; } // "2025-01", "2025-Q1", "2025" 형태로 저장
        public decimal Amount { get; set; } // 해당 기간의 총 매출 또는 수수료
        public string EmployeeName { get; set; }

    }
}
