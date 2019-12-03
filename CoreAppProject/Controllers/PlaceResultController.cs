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
    [Route("api/places")]
    [ApiController]
    public class PlaceResultController : ControllerBase
    {
        private readonly IPlaceService placeService;

        public PlaceResultController(IPlaceService _placeService)
        {
            placeService = _placeService;
        }

        // GET: api/PlaceResult
        [HttpGet]
        [Route("getall/{city}")]
        public List<PlaceModel> GetPlaceList(string city)
        {
            List<PlaceModel> placesList = this.placeService.GetPlacesByCity(city);
            return placesList;
        }

        [HttpPost]
        [Route("add")]
        public void AddPlace([FromBody] PlaceModel model)
        {
            this.placeService.AddNewPlace(model);
        }

    }
}
