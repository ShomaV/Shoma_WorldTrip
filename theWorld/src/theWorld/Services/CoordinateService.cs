using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theWorld.Services
{
    //using System.Net;
    //using System.Net.Http;

    using System.Net;
    using System.Net.Http;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json.Linq;

    public class CoordinateService
    {
        private readonly ILogger<CoordinateService> _logger;

        public CoordinateService(ILogger<CoordinateService> logger)
        {
            _logger = logger;
        }

        public  async Task<CoordinateServiceResult> Lookup(string location)
        {
            var result = new CoordinateServiceResult()
            {
                Success = false,
                Message = "Undetermined Failure while looking up coordinates",
            };
            //lookup coorodinates
            var encodedName = WebUtility.UrlDecode(location);
            var bingKey = Startup.Configuration["AppSettings:BingKey"];
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={bingKey}";
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find {location} as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find confidence match for {location} as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude= (double)coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }
}
