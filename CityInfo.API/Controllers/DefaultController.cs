using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private CityInfoContext _ctx;
        public DefaultController(CityInfoContext ctx)
        {
            this._ctx = ctx;
        }
        // GET: api/Default
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

      
    }
}
