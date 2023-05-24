namespace CarPoolingWebAPI.DTO
{
    public class BookingDetailsDBO
    {
        public int BookingId { get; set; }
        public int RideId { get; set; }
        public Guid CustomerId { get; set; }
        public string BoardingStop { get; set; }
        public string DestinationStop { get; set; }
        public int SeatsBooked { get; set; }
        public int Fair { get; set; }
        public int TotalFair { get; set; }
        public DateTime Date { get; set; }
    }
}
