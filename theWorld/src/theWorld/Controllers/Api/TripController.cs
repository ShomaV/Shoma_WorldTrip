using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theWorld.Controllers.Api
{
    using System.Linq.Expressions;
    using System.Net;

    using AutoMapper;

    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Server.Kestrel.Http;
    using Microsoft.Extensions.Logging;

    using theWorld.Models;
    using theWorld.ViewModels;

    [Authorize]
    [Route("api/trips")]
    public class TripController:Controller
    {
        private readonly ILogger<TripController> _logger;
        private readonly IWorldRepository _repository;

        public TripController(IWorldRepository repository,ILogger<TripController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = this._repository.GetUserTripsWithStops(this.User.Identity.Name);
            var results = Mapper.Map<IEnumerable<TripViewModel>>(trips);
            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vmModel)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vmModel);
                    newTrip.UserName = this.User.Identity.Name;
                    //save it to the database
                    this._repository.AddTrip(newTrip);
                    if (this._repository.SaveAll())
                    {
                        this._logger.LogInformation("Attempting to save a new trip");
                        this.Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }                    
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Failed to save new trip", ex);
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new {Message=ex.Message});
                
            }
            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.Json(new { Message = "Failed", ModelState= this.ModelState});

        }
    }
}
