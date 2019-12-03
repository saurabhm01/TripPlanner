using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using Tripzz.Data;
using Tripzz.Entity;
using Tripzz.Service.Interface;
using System.Linq;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Tripzz.Service
{
    public class PlaceService : IPlaceService
    {
        private IRepository<PlaceModel> placeRepository;

        private IRepository<CityModel> cityRepository;

        public PlaceService(IRepository<PlaceModel> _placeRepository, IRepository<CityModel> cityRepository)
        {
            this.placeRepository = _placeRepository;
            this.cityRepository = cityRepository;
        }

        public bool AddNewPlace(PlaceModel model)
        {
            try
            {
                PlaceModel newPlace = new PlaceModel();
                newPlace.Name = model.Name;
                newPlace.PlaceDescription = model.PlaceDescription;
                newPlace.Address = model.Address;
                newPlace.ImageUrl = model.ImageUrl;
                newPlace.Tips = model.Tips;
                newPlace.Distance = model.Distance;
                newPlace.BestTimeToVisit = model.BestTimeToVisit;
                var latlongDetail = GetLatAndLongDetail(model.Address);
                if (latlongDetail != null)
                {
                    newPlace.Latitude = latlongDetail.Item1;
                    newPlace.Longitude = latlongDetail.Item2;
                }
                if (!string.IsNullOrWhiteSpace(model.CityName))
                {
                    var cityLatLongDetail = GetLatAndLongDetail(model.CityName);
                    if (cityLatLongDetail != null)
                    {
                        var existingCityDetail = cityRepository.GetAll().Where(t => t.Name.Trim().Equals(model.CityName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        if (existingCityDetail == null)
                        {
                            CityModel newCity = new CityModel();
                            newCity.Name = model.CityName;
                            newCity.Latitude = cityLatLongDetail.Item1;
                            newCity.Longitude = cityLatLongDetail.Item2;
                            if (cityRepository.Insert(newCity))
                            {
                                newPlace.CityId = newCity.Id;
                            }
                        }
                    }
                }
                return this.placeRepository.Insert(newPlace);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding new place due to:" + ex);
            }

        }

        private Tuple<string, string> GetLatAndLongDetail(string address)
        {
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(address), "AIzaSyAyEqhZkIjL-Zii1vqi6_LAdTXqspno3T8");

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");

            //string url = "https://maps.google.com/maps/api/geocode/xml?address=" + address + "&sensor=false&key=AIzaSyAyEqhZkIjL-Zii1vqi6_LAdTXqspno3T8";
            //WebRequest request = WebRequest.Create(url);
            //using (WebResponse response = (HttpWebResponse)request.GetResponse())
            //{
            //    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            //    {
            //        DataSet dsResult = new DataSet();
            //        dsResult.ReadXml(reader);

            //        foreach (DataRow row in dsResult.Tables["result"].Rows)
            //        {
            //            string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
            //            DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
            //            if (location["lat"] != null && location["lng"] != null)
            //            {
            //                return new Tuple<string, string>(location["lat"].ToString(), location["lng"].ToString());
            //            }
            //        }
            //    }
            //}
            return null;
        }

        public List<PlaceModel> GetAllPlaces()
        {
            try
            {
                List<PlaceModel> placeList = new List<PlaceModel>();
                var placeData = this.placeRepository.GetAll();
                if (placeData != null)
                {
                    foreach (var data in placeData)
                    {
                        PlaceModel placeModel = new PlaceModel();
                        placeModel.Name = data.Name;
                        placeModel.PlaceDescription = data.PlaceDescription;
                        placeModel.Address = data.Address;
                        placeModel.ImageUrl = data.ImageUrl;
                        placeModel.Tips = data.Tips;
                        placeModel.Distance = data.Distance;
                        placeModel.BestTimeToVisit = data.BestTimeToVisit;
                        placeModel.Latitude = data.Latitude;
                        placeModel.Longitude = data.Longitude;
                        placeList.Add(placeModel);
                    }
                }
                return placeList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while get all place list due to:" + ex);
            }
        }

        public List<PlaceModel> GetPlacesByCity(string cityName)
        {
            try
            {
                List<PlaceModel> placeList = new List<PlaceModel>();
                List<SqlParameter> sqlParamsList = new List<SqlParameter>()
                {
                    new SqlParameter("@city ",cityName)
                };

                var placesList = placeRepository.ExecuteStoredProcedure("usp_GetPlaceListByCity", sqlParamsList);
                if (placesList != null)
                {
                    using (placesList)
                    {
                        placeList = DataReaderMapper.MapToList<PlaceModel>(placesList);
                    }

                    //foreach (var data in placesList)
                    //{
                    //    PlaceModel placeModel = new PlaceModel();
                    //    placeModel.Name = data.Name;
                    //    placeModel.PlaceDescription = data.PlaceDescription;
                    //    placeModel.Address = data.Address;
                    //    placeModel.ImageUrl = data.ImageUrl;
                    //    placeModel.Tips = data.Tips;
                    //    placeModel.Distance = data.Distance;
                    //    placeModel.BestTimeToVisit = data.BestTimeToVisit;
                    //    placeModel.Latitude = data.Latitude;
                    //    placeModel.Longitude = data.Longitude;
                    //    placeList.Add(placeModel);
                    //}
                }
                return placeList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while get all place list due to:" + ex);
            }
        }
    }
}
