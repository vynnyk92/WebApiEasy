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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "Point")]
        public IActionResult Point(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            return Ok(city);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePoint(int cityId, [FromBody]PointOfInterestCreationDTO pointOfInterestCreationDTO)
        {
            if (pointOfInterestCreationDTO == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            if (pointOfInterestCreationDTO.Name == pointOfInterestCreationDTO.Description)
            {
                ModelState.AddModelError("Description", "Description musr be differ");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxPointOfInterstsCount = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

            var newPoint = new PointOfInterestDTO
            {
                Id = ++maxPointOfInterstsCount,
                Name = pointOfInterestCreationDTO.Name,
                Description = pointOfInterestCreationDTO.Description
            };

            city.PointOfInterest.Add(newPoint);

            return CreatedAtRoute("Point", new { cityId = city.Id, id = newPoint.Id }, newPoint);
        }

        //[HttpPut("{cityId}/pointsofinterest/{id}")]
        //public IActionResult UpdatePoint(int cityId, int id, [FromBody]PointOfInterestCreationDTO pointOfInterestCreationDTO)
        //{

        //}
    }
}