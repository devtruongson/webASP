namespace webASP.DTO
{
    public class RentedBikeDTO
    {
        public int Id { get; set; }
        public string TimeOrder { get; set; }
        public string ExpriedCar { get; set; }
        public string PriceTotal { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string Model { get; set; }
        public string Thumbnail { get; set; }

        public RentedBikeDTO(int id, string timeOrder, string expriedCar, string priceTotal, string customerEmail, string customerPhoneNumber, string model, string thumbnail)
        {
            Id = id;
            TimeOrder = timeOrder;
            ExpriedCar = expriedCar;
            PriceTotal = priceTotal;
            CustomerEmail = customerEmail;
            CustomerPhoneNumber = customerPhoneNumber;
            Model = model;
            Thumbnail = thumbnail;
        }
    }
}
