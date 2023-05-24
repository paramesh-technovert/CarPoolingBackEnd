using System;
using System.Collections.Generic;

namespace CarPoolingWebAPI.Models;

public partial class OfferRide
{
    public int RideId { get; set; }

    public Guid RideProviderId { get; set; }

    public int SourceId { get; set; }

    public int DestinationId { get; set; }

    public DateTime Date { get; set; }

    public int TotalSeats { get; set; }

    public int Fair { get; set; }

    public int Distance { get; set; }

    public virtual ICollection<BookedRide> BookedRides { get; } = new List<BookedRide>();

    public virtual City Destination { get; set; } = null!;

    public virtual UserDetail RideProvider { get; set; } = null!;

    public virtual City Source { get; set; } = null!;

    public virtual ICollection<Stop> Stops { get; } = new List<Stop>();
}
