namespace CarPoolingWebAPI.DTO
{
    public class BookedRidesDT
    {
        public Guid RideProviderId { get; set; }
        public string RideProviderName { get; set; }
        public string BoardingStop { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public int price { get; set; }
        public int SeatsBooked { get; set; }
        public string Image { get; set; }
    }
}
