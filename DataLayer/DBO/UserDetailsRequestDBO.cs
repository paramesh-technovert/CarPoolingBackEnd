namespace CarPoolingWebAPI.DTO
{
    public class UserDetailsRequestDBO
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public decimal? PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }
    }
}
