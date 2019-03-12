using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet("")]
        public IActionResult GetCities()
        {
            return Ok(CitiInMemory.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            var city = CitiInMemory.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}