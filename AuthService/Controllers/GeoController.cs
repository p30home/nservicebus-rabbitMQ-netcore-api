using System;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;
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
        private AppDbContext _dbContext;
        private BusService _busService;


        public GeoController(AppDbContext dbContext, BusService busService)
        {
            _dbContext = dbContext;
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
            _dbContext.ResultHistories.Add(new ResultHistory
            {
                UserId = User.Identity.Name,
                DistanceResult = geoLineResponse.Distance,
                FromLat = geoLineResponse.FromLat,
                FromLong = geoLineResponse.FromLong,
                ToLat = geoLineResponse.ToLat,
                ToLong = geoLineResponse.ToLong
            });

            _dbContext.SaveChanges();
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
        public IActionResult History()
        {
            var histories = _dbContext.ResultHistories.Where(c => c.UserId == User.Identity.Name).Select(c => new
            {
                c.FromLong,
                c.FromLat,
                c.ToLong,
                c.ToLat,
                c.DistanceResult
            }).ToList();

            return Ok(histories);
        }

    }
}