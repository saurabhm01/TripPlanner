using System;
using System.Collections.Generic;
using System.Text;
using Tripzz.Data;
using Tripzz.Entity;
using Tripzz.Service.Interface;

namespace Tripzz.Service
{
    public class CityService : ICityService
    {
        private readonly IRepository<CityModel> cityRepository;

        public CityService(IRepository<CityModel> _cityRepostiry)
        {
            this.cityRepository = _cityRepostiry;
        }

        public List<CityModel> GetAllCities()
        {
            try
            {
                List<CityModel> cityModels = new List<CityModel>();
                var cityList = this.cityRepository.GetAll();
                if (cityList != null)
                {
                    foreach(var data in cityList)
                    {
                        CityModel city = new CityModel();
                        city.Name = data.Name;
                        city.Latitude = data.Latitude;
                        city.Longitude = data.Longitude;
                        cityModels.Add(city);
                    }
                }
                return cityModels;
            }
            catch(Exception ex)
            {
                throw new Exception("Error while get all city list due to:" + ex);
            }
        }
    }
}
