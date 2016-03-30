using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theWorld.Models
{
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Extensions.Logging;

    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
        {            
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return this._context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                this._logger.LogError("Could not get trips from database.",ex);
                return null;
            }
            
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            return this._context.Trips
                .Include(t=>t.Stops)
                .OrderBy(t => t.Name)
                .ToList();
        }

        public void AddTrip(Trip newTrip)
        {
            this._context.Add(newTrip);
        }

        public bool SaveAll()
        {
            return this._context.SaveChanges() > 0;
        }

        public Trip GetTripByName(string tripName, string name)
        {
            return this._context.Trips.Include(t => t.Stops)
                .Where(t => t.Name == tripName && t.UserName==name)
                .FirstOrDefault();
        }

        public void AddStop(string tripName, string userName, Stop newStop)
        {
            var theTrip = GetTripByName(tripName,userName);
            newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            theTrip.Stops.Add(newStop);
            this._context.Stops.Add(newStop);
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return this._context.Trips                   
                .Include(t => t.Stops)
                .OrderBy(t => t.Name)
                .Where(t => t.UserName == name)
                .ToList();
            }
            catch (Exception exception)
            {
                this._logger.LogError("Unable to find trips", exception);
                return null;
            }
        }
    }
}
