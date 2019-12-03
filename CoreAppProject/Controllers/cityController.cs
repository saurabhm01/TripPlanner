using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tripzz.Entity;
using Tripzz.Service.Interface;

namespace Trippzz.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService _cityService)
        {
            this.cityService = _cityService;
        }

        [HttpGet]
        [Route("getall")]
        public List<CityModel> GetCityList()
        {
            var cityList = this.cityService.GetAllCities();
            return cityList;
        }
    }
}