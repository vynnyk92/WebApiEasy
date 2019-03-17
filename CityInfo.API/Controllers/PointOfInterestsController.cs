using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;
using CityInfo.API.Repositories;
using CityInfo.API.Entities;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestsController : Controller
    {
        ICityInfoRepository cityInfoRepository;
      
        public ILogger<PointOfInterestsController> _logger { get; set; }
        private IMailService _localMailService;

        public PointOfInterestsController(ILogger<PointOfInterestsController> logger, IMailService localMailService, ICityInfoRepository repository)
        {
            _logger = logger;
            _localMailService = localMailService;
            cityInfoRepository = repository;
            //HttpContext.RequestServices.GetService();
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult Points(int cityId)
        {
            try
            {

                throw new Exception("Example error");

                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation("City wasnt found");
                    return NotFound();
                }
                return Ok(city.PointOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Example error");
            }

          
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "Point")]
        public IActionResult Point(int cityId, int id)
        {
            var city = cityInfoRepository.GetCity(cityId);

            if (city == null)
            {
                return NotFound();
            }
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            return Ok(point);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePoint(int cityId, [FromBody]PointOfInterestCreationDTO pointOfInterestCreationDTO)
        {
            if (pointOfInterestCreationDTO == null)
            {
                return BadRequest();
            }

            var city = cityInfoRepository.GetCity(cityId);

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

            var pointOfInterest = AutoMapper.Mapper.Map<PointOfInterest>(pointOfInterestCreationDTO);

            cityInfoRepository.AddPointOfInterest(cityId, pointOfInterest);

            return CreatedAtRoute("Point", new { cityId = city.Id, id = pointOfInterest.Id }, pointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePoint(int cityId, int id, [FromBody]PointOfInterestUpdateDTO pointOfInterestUpdateDTO)
        {
            if (pointOfInterestUpdateDTO == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (city == null || point == null)
            {
                return NotFound();
            }
            if (pointOfInterestUpdateDTO.Name == pointOfInterestUpdateDTO.Description)
            {
                ModelState.AddModelError("Description", "Description musr be differ");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            point.Name = pointOfInterestUpdateDTO.Name;
            point.Description = pointOfInterestUpdateDTO.Description;

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePoint(int cityId, int id, [FromBody]JsonPatchDocument<PointOfInterestUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (city == null || point == null)
            {
                return NotFound();
            }

            var poinOfIntToPatch = new PointOfInterestUpdateDTO()
            {
                Name = point.Name,
                Description = point.Description
            };

            patchDoc.ApplyTo(poinOfIntToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (poinOfIntToPatch.Name == poinOfIntToPatch.Description)
            {
                ModelState.AddModelError("Description", "Description musr be differ");
            }

            TryValidateModel(point);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            point.Name = poinOfIntToPatch.Name;
            point.Description = poinOfIntToPatch.Description;
            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePoint(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (city == null || point == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(point);

            _localMailService.Send($"{point.Name} has been deleted", $"{point.Name} has been deleted");

            return NoContent();
        }
    }
}