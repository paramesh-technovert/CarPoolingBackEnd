namespace CarPoolingWebAPI.DTO
{
    public class BookRideResponseDT
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public int RideId { get; set; }
        public string BoardingPoint { get; set; }
        public string DroppingPoint { get; set; }
        public DateTime Date { get; set; }
        public int SeatsBooked { get; set; }
        public int Fair { get; set; }
        public int TotalAmount { get; set; }
    }
}
