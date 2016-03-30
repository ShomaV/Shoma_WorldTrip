using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theWorld.Controllers.Api
{
    using System.Net;

    using AutoMapper;

    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.Logging;

    using theWorld.Models;
    using theWorld.Services;

    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private readonly CoordinateService _coordinateService;
        private readonly ILogger _logger;
        private readonly IWorldRepository _repository;

        public StopController(IWorldRepository repository, ILogger<StopController> logger, CoordinateService coordinateService)
        {
            _repository = repository;
            _logger = logger;
            _coordinateService = coordinateService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = this._repository.GetTripByName(tripName, this.User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops));
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Failed to get stops for {tripName}", ex);
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error in finding trip name");
            }
        }

        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel vmModel)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    //map tp the entity
                    var newStop = Mapper.Map<Stop>(vmModel);
                    
                    //looking at geo co-ordinates
                    var coordService = await this._coordinateService.Lookup(newStop.Name);
                    if (!coordService.Success)
                    {
                        this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordService.Message);
                    }
                    //save to db
                    newStop.Latitude = coordService.Latitude;
                    newStop.Longitude = coordService.Longitude;
                    this._repository.AddStop(tripName, this.User.Identity.Name, newStop);
                    if (this._repository.SaveAll())
                    {
                        this._logger.LogInformation("Attempting to save a new stop");
                        this.Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Failed to save new stop", ex);
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop");
            }
            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to save new stop");
        }
    }
}
