namespace CarPoolingWebAPI.DTO
{
    public class OfferedRidesDTO
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string BoardingStop { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public int price { get; set; }
        public int SeatsBooked { get; set; }
        public string Image { get; set; }
    }
}
