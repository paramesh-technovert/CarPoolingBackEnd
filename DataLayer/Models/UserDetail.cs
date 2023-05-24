using System;
using System.Collections.Generic;

namespace CarPoolingWebAPI.Models;

public partial class UserDetail
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public decimal? PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<BookedRide> BookedRides { get; } = new List<BookedRide>();

    public virtual LoginDetail IdNavigation { get; set; } = null!;

    public virtual ICollection<OfferRide> OfferRides { get; } = new List<OfferRide>();
}
