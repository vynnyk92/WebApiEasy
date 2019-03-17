using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using CityInfo.API.Entities;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        ICityInfoRepository cityInfoRepository;
        public CitiesController(ICityInfoRepository repository)
        {
            this.cityInfoRepository = repository;
        }

        [HttpGet("")]
        public IActionResult GetCities()
        {
            var cities = cityInfoRepository.GetCities();
            var result = AutoMapper.Mapper.Map<IEnumerable<CityDTO>>(cities);


            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            var city = cityInfoRepository.GetCity(id);
            if (city == null)
            {
                return NotFound();
            }
            var result = AutoMapper.Mapper.Map<CityDTO>(city);


            return Ok(result);
        }
    }
}