using System;
using System.Collections.Generic;

namespace CarPoolingWebAPI.Models;

public partial class BookedRide
{
    public int BookingId { get; set; }

    public int RideId { get; set; }

    public Guid CustomerId { get; set; }

    public int BoardingStop { get; set; }

    public int DestinationStop { get; set; }

    public int SeatsBooked { get; set; }

    public int Fair { get; set; }

    public DateTime Date { get; set; }

    public virtual City BoardingStopNavigation { get; set; } = null!;

    public virtual UserDetail Customer { get; set; } = null!;

    public virtual City DestinationStopNavigation { get; set; } = null!;

    public virtual OfferRide Ride { get; set; } = null!;
}
