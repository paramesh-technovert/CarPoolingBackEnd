using System;
using System.Collections.Generic;

namespace CarPoolingWebAPI.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<BookedRide> BookedRideBoardingStopNavigations { get; } = new List<BookedRide>();

    public virtual ICollection<BookedRide> BookedRideDestinationStopNavigations { get; } = new List<BookedRide>();

    public virtual ICollection<OfferRide> OfferRideDestinations { get; } = new List<OfferRide>();

    public virtual ICollection<OfferRide> OfferRideSources { get; } = new List<OfferRide>();

    public virtual ICollection<Stop> Stops { get; } = new List<Stop>();
}
