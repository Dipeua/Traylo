namespace Traylo.Models
{
    public class DashboardViewModel
    {
        public List<City> Cites { get; set; }
        public List<DeliveryPerson> DeliveryPersons { get; set; }
        public int TotalProducts { get; set; }
        public List<StockHistory> StockHistorys { get; set; }
    }
}
