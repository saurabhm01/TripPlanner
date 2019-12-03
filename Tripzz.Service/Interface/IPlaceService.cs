using System;
using System.Collections.Generic;
using System.Text;
using Tripzz.Entity;

namespace Tripzz.Service.Interface
{
    public interface IPlaceService
    {
        bool AddNewPlace(PlaceModel model);
        List<PlaceModel> GetAllPlaces();

        List<PlaceModel> GetPlacesByCity(string cityName);
    }
}
