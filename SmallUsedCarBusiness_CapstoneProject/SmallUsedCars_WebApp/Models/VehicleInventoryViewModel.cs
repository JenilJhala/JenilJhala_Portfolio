namespace SmallUsedCars_WebApp.Models
{
    public class VehicleInventoryViewModel
    {
        public int VehicleId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public string Mileage { get; set; }
        public string Status { get; set; }
        public decimal MarketValue { get; set; }

        // 거래 유형을 추가하여 보유중인 전체 차량 리스트를 보여주면서, 고객의 차량을 구입하기 위해 DB에 막 등록(등록을 먼저하는 이유는 DB에 등록을 안하면 차량아이디 값이 없어서 입력필드에 정보 입력후 DB에 저장시 해당i d를 가진 차량을 찾지 못해서 에러 발생) 한 차량임을 표시하기 위해 "Pending" 으로 표시해서 직원에게 마저 거래정보를 입력완료하도록 알림
        public string TransactionType { get; set; }
        public string? ImageFileName { get; set; }

    }
}
