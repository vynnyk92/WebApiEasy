using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestsController : Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult Points(int cityId)
        {
            var city = CitiInMemory.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}")]
        public IActionResult Point(int cityId, int id)
        {
            var city = CitiInMemory.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            city.PointOfInterest.FirstOrDefault(p=>p.Id==id);
            return Ok(city);
        }
    }
}