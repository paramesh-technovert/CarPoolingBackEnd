namespace CarPoolingWebAPI.DTO
{
    public class BookRideRequestDTO
    {
        public Guid Id { get; set; }
        public int RideId { get; set; }
        public DateTime DateTime { get; set; }
        public int BoardingStopId { get; set; }
        public int DestinationStopId { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }
    }
}
