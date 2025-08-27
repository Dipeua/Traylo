namespace Traylo.Models
{
    public class DeliveryTableViewModel
    {
        public string ProductName { get; set; }
        public Dictionary<string, Dictionary<string, int>> QuantitiesByCityAndDeliveryPerson { get; set; }
        // CityName -> (DeliveryPersonName -> Quantity)
    }

}
