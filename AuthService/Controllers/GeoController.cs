using System;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Services;
using AuthService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Shared;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeoController : ControllerBase
    {

        private BusService _busService;


        public GeoController(BusService busService)
        {

            _busService = busService;
        }


        [HttpPost("CalculateDistance")]
        public async Task<IActionResult> CalculateDistance([FromBody]GeoPointsViewModel model)
        {
            var geoLineRequest =
                new GeoLineRequest
                {
                    FromLat = model.FromLat,
                    FromLong = model.FromLong,
                    ToLat = model.ToLat,
                    ToLong = model.ToLong,
                    UserId = User.Identity.Name
                };

            var geoLineResponse = await _busService.SendGeoLineRequest(geoLineRequest);

            await _busService.SaveGeoLineResult(new GeoLineResult
            {
                UserId = User.Identity.Name,
                Distance = geoLineResponse.Distance,
                FromLat = geoLineResponse.FromLat,
                FromLong = geoLineResponse.FromLong,
                ToLat = geoLineResponse.ToLat,
                ToLong = geoLineResponse.ToLong
            });


            return Ok(new
            {
                FromLat = model.FromLat,
                FromLong = model.FromLong,
                ToLat = model.ToLat,
                ToLong = model.ToLong,
                Distance = geoLineResponse.Distance
            });
        }

        [HttpGet("history")]
        public async Task<IActionResult> History()
        {
            var result = await _busService.GetGeoLineHistories(new GetGeoLineHistory { UserId = User.Identity.Name });
            var histories = result.GeoLines.Select(c => new
            {
                c.FromLong,
                c.FromLat,
                c.ToLong,
                c.ToLat,
                c.Distance
            }).ToList();

            return Ok(histories);
        }

    }
}