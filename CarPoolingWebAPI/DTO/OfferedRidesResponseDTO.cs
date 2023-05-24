namespace CarPoolingWebAPI.DTO
{
    public class OfferedRidesResponseDTO
    {
        public int RideId { get; set; }
        public Guid RideOwnerId { get; set; }

        public String StartingStop { get; set; }

        public String EndingStop { get; set; }

        public DateTime Date { get; set; }

        public int Distance { get; set; }

        public int Fair { get; set; }

        public int TotalSeats { get; set; }
    }
}
