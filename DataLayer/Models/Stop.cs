using System;
using System.Collections.Generic;

namespace CarPoolingWebAPI.Models;

public partial class Stop
{
    public int Id { get; set; }

    public int RideId { get; set; }

    public int StopId { get; set; }

    public int AvailableSeats { get; set; }

    public DateTime Date { get; set; }

    public virtual OfferRide Ride { get; set; } = null!;

    public virtual City StopNavigation { get; set; } = null!;
}
