﻿namespace CarPoolingWebAPI.DTO
{
    public class MatchedRidesRequestDBO
    {
        public Guid Id { get; set; }
        public string BoardingPoint { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public int SeatsRequired { get; set; }
    }
}
