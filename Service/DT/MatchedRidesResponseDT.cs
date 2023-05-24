
namespace CarPoolingWebAPI.DTO
{
    public class MatchedRidesResponseDT
    {
        public Guid Id { get; set; }
        public int RideId { get; set; }
        public int AvailableSeats { get; set; }
        public int BoardingStopId { get; set; }
        public int DestinationStopId { get; set; }
        public DateTime DateTime { get; set; }
        public int Price { get; set; }
        public string RideProviderName { get; set; }
        public string Image { get; set; }
    }
}
