namespace YourNamespace.Models
{
    public class BookingForm
    {
        public int LocationId { get; set; }
        public DateTime FirstReceived { get; set; }
        public DateTime DateReturnCar { get; set; }
        public int CustomerId { get; set; }
    }
}
