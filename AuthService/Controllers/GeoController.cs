using System;
using System.Linq;
using AuthService.Models;
using AuthService.Services;
using AuthService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeoController : ControllerBase
    {
        private AppDbContext _dbContext;

        public GeoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost("CalculateDistance")]
        public IActionResult CalculateDistance([FromBody]GeoPointsViewModel model)
        {
            var result = distance(model.FromLat, model.FromLong, model.ToLat, model.ToLong);
            _dbContext.ResultHistories.Add(new ResultHistory
            {
                UserId = User.Identity.Name,
                DistanceResult = result,
                FromLat = model.FromLat,
                FromLong = model.FromLong,
                ToLat = model.ToLat,
                ToLong = model.ToLong
            });

            _dbContext.SaveChanges();
            return Ok(result);
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


        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        /// <summary>
        /// calculates distance between two points and returns result as Killometers
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        private double distance(double lat1, double lon1, double lat2, double lon2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;

                dist = dist * 1.609344;

                return (dist);
            }
        }
    }
}