using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace theWorld.Models
{
    using Microsoft.AspNet.Identity;

    public class WorldCotextSeedData
    {
        private WorldContext _context;
        private readonly UserManager<WorldUser> _userManager;

        public WorldCotextSeedData(WorldContext context, UserManager<WorldUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task EnsureSeedDataAsync()
        {
            if (await this._userManager.FindByEmailAsync("sai.ram@theWorld.com") == null)
            {
                var newUser=new WorldUser()
                                {
                                    UserName = "sairam",
                                    Email = "sai.ram@theWorld.com"
                };
                await this._userManager.CreateAsync(newUser, "P@ssw0rd!");                               
            }
                       
            if (!this._context.Trips.Any())
            {
                //add new data
                var usTrip = new Trip()
                {
                    Name = "US Trip",
                    Created = DateTime.Now,
                    UserName = "sairam",
                    Stops = new List<Stop>()
                                                 {
                                                     new Stop()
                                                         {
                                                             Name = "Atlanta,GA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 33.748995,
                                                             Longitude = -84.387982,
                                                             Order = 0
                                                         },new Stop()
                                                         {
                                                             Name = "New York,NY",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 40.712784,
                                                             Longitude = -74.005941,
                                                             Order = 1
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Boston,MA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 42.360082,
                                                             Longitude = -7105880,
                                                             Order = 2
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Chicago,IL",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 41.878114,
                                                             Longitude = -87.629798,
                                                             Order = 3
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Seattle,WA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 47.606209,
                                                             Longitude = -122.332071,
                                                             Order = 4
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Atlanta,GA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 33.748995,
                                                             Longitude = -84.387982,
                                                             Order = 5
                                                         }

                                                 }
                };

                this._context.Trips.Add(usTrip);
                this._context.Stops.AddRange(usTrip.Stops);
                var worldTrip = new Trip()
                {
                    Name = "World Trip",
                    Created = DateTime.Now,
                    UserName = "sairam",
                    Stops = new List<Stop>()
                    {
                        new Stop()
                                                         {
                                                             Name = "Atlanta,GA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 33.748995,
                                                             Longitude = -84.387982,
                                                             Order = 0
                                                         },new Stop()
                                                         {
                                                             Name = "New York,NY",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 40.712784,
                                                             Longitude = -74.005941,
                                                             Order = 1
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Boston,MA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 42.360082,
                                                             Longitude = -7105880,
                                                             Order = 2
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Chicago,IL",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 41.878114,
                                                             Longitude = -87.629798,
                                                             Order = 3
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Seattle,WA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 47.606209,
                                                             Longitude = -122.332071,
                                                             Order = 4
                                                         },
                                                     new Stop()
                                                         {
                                                             Name = "Atlanta,GA",
                                                             Arrival = new DateTime(2015,12,25),
                                                             Latitude = 33.748995,
                                                             Longitude = -84.387982,
                                                             Order = 5
                                                         }
                    }
                };
                this._context.Trips.Add(worldTrip);
                this._context.Stops.AddRange(worldTrip.Stops);

                this._context.SaveChanges();
            }
        }
    }
}
