using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace CarPoolingWebAPI.Repository
{
    public class Stops : IStops
    {
        private readonly CarPoolingDbContext _dbContext;
        public Stops(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public int GetStopId(int id)
        {
            return _dbContext.Stops.First(e => e.Id == id).StopId;
        }
        public async Task AddStops(Stop stop)
        {
            try
            {
                await _dbContext.Stops.AddAsync(stop);
                // await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void BookingRide(BookRideRequestDBO bookRideRequestDTO)
        {
            try
            {
                IQueryable<Stop> stops = _dbContext.Stops.Where(r => r.RideId == bookRideRequestDTO.RideId && r.Id >= bookRideRequestDTO.BoardingStopId && r.Id < bookRideRequestDTO.DestinationStopId);
                foreach (Stop stop in stops)
                {
                    if (stop.AvailableSeats < bookRideRequestDTO.BookedSeats)
                    {
                        throw new Exception("Seats not available");
                    }
                    stop.AvailableSeats -= bookRideRequestDTO.BookedSeats;
                }
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Unable to book the ride");
            }
        }

        public async Task<List<MatchedRidesResponseDBO>> GetMatchedRides(MatchedRidesRequestDBO matchedRidesRequestDTO)
        {
            Cities cities = new Cities(_dbContext);

            int boardingPoint = await cities.GetCityID(matchedRidesRequestDTO.BoardingPoint);
            int destination = await cities.GetCityID(matchedRidesRequestDTO.Destination);
            //var st=(from s1 in _dbContext.Stops join s2 in _dbContext.Stops on s1.RideId equals s2.RideId where s1.Id<s2.Id && s1.StopId==BoardingPoint && s2.StopId==Destination && s1.Date==Date && s1.StartTime==Time select new { s1.RideId, Index1 = s1.Id, Index2 = s2.Id }).Join(_dbContext.Stops,s1=>s1.RideId,s2=>s2.RideId,(s1,s2)=>new {s1,s2 }).Where(x=>x.s2.Id>=x.s1.Index1 && x.s2.Id<x.s1.Index2).GroupBy(x=>x.s1.RideId).Select(g=>new {RideId=g.Key,MaxAvailableSeats=g.Min(x=>x.s2.AvailableSeats)}) ;
            //var e= (from s in _dbContext.Stops join s1 in (from s1 in _dbContext.Stops join s2 in _dbContext.Stops on s1.RideId equals s2.RideId where s1.Id < s2.Id && s1.StopId == BoardingPoint && s2.StopId == Destination && s1.Date == Date && s1.StartTime == Time select new { s1.RideId, Index1 = s1.Id, Index2 = s2.Id }) on s.RideId equals s1.RideId where s.Id >= s1.Index1 && s.Id < s1.Index2 group s by s.RideId into g select new { RideId = g.Key, MaxAvailableSeats = g.Min(s => s.AvailableSeats), Index1 = g.Min(s => s.Index1), Index2 = g.Max(s => s.Index2) });
            var result= (
                        from s1 in _dbContext.Stops
                        join s2 in _dbContext.Stops on s1.RideId equals s2.RideId
                        where s1.Id < s2.Id && s1.StopId == boardingPoint && s2.StopId == destination && s1.Date == matchedRidesRequestDTO.Date
                        select new { s1.RideId, Index1 = s1.Id, Index2 = s2.Id }
                    ).Join(
                        _dbContext.Stops,
                        s1 => s1.RideId,
                        s => s.RideId,
                        (s1, s) => new { s1, s }
                    ).Where(
                        x => x.s.Id >= x.s1.Index1 && x.s.Id < x.s1.Index2
                    ).GroupBy(
                        x => x.s.RideId
                    ).Select(
                        g => new MatchedRidesResponseDBO
                        {
                            RideId = g.Key,
                            AvailableSeats = g.Min(x => x.s.AvailableSeats),
                            BoardingStopId = g.First().s1.Index1,
                            DestinationStopId = g.First().s1.Index2,
                            DateTime = matchedRidesRequestDTO.Date
                        }
                    );
            var res=(from i in (from o in _dbContext.OfferRides
                   join u in _dbContext.UserDetails on o.RideProviderId equals u.Id where u.Id!=matchedRidesRequestDTO.Id select new { RideProviderId=u.Id,RideProviderName = u.FirstName.Trim() + " " + u.LastName.Trim(), Price = o.Fair,RideId=o.RideId,Image=u.ImageUrl })
                   join r in result on i.RideId equals r.RideId
                   select new MatchedRidesResponseDBO
                   {
                       Id = i.RideProviderId,
                       RideProviderName = i.RideProviderName,
                       AvailableSeats = r.AvailableSeats,
                       DateTime = r.DateTime,
                       Price = i.Price,
                       RideId = r.RideId,
                       BoardingStopId=r.BoardingStopId,
                       DestinationStopId=r.DestinationStopId,
                       Image=i.Image
                   }).ToList();
            return res;
        }
        
    }
}
