using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.Models
{
    public class transactionInfoViewModel
    {
        public int VehicleId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        //  기본값을 0으로 설정하여 필수 입력이 아니도록 변경
        [Range(0, double.MaxValue, ErrorMessage = "Purchase Price cannot be negative.")]
        public decimal PurchasePrice { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Sales Price cannot be negative.")]
        public decimal SalesPrice { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Trade-In Value cannot be negative.")]
        public decimal TradeInValue { get; set; } = 0;

        public string? EmployeeId { get; set; }

        
    }
}
