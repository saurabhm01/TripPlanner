using System;
using System.Collections.Generic;
using System.Text;
using Tripzz.Entity;

namespace Tripzz.Service.Interface
{
    public interface ICityService
    {
        List<CityModel> GetAllCities();
    }
}
