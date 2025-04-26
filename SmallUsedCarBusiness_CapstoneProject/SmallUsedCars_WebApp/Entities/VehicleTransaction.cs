namespace SmallUsedCars_WebApp.Entities
{
    public class VehicleTransaction
    {
        public int VehicleTransactionId { get; set; } // 기본 키

        public string TransactionType { get; set; } // 거래 유형 (구매, 판매 등)
        public DateTime TransactionDate { get; set; } // 거래 날짜

        public decimal PurchasePrice { get; set; } // 구매 가격
        public decimal SalesPrice { get; set; } // 판매 가격
        public decimal TradeInValue { get; set; } // 트레이드인 값
        public decimal FinalPrice { get; set; } // 최종 가격
        public decimal MarginRate { get; set; } // 마진율

        //  중복된 Vehicle 속성 제거 (Manufacturer, Model, MarketValue, TransactionPrice 삭제)

        //  Foreign Key (Vehicle과 관계 설정)
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } //  Vehicle 네비게이션 추가

        //  Foreign Key (거래 담당 직원)
        public string? EmployeeId { get; set; }
        public Employee? Employee { get; set; } //  Employee 네비게이션 추가
    }
}
