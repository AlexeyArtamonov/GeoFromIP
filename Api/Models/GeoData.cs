using System.Collections.Generic;
using System.Linq;

namespace Api.Models
{
    public class GeoData
    {
        public GeoData(MaxMind.GeoIP2.Responses.CityResponse cityResponse)
        {
            Continent   = cityResponse.Continent.Names.ToDictionary (pair => pair.Key, pair => pair.Value);
            Country     = cityResponse.Country.Names.ToDictionary   (pair => pair.Key, pair => pair.Value);
            City        = cityResponse.City.Names.ToDictionary      (pair => pair.Key, pair => pair.Value);
            Latitude    = cityResponse.Location.Latitude ?? double.NaN;
            Longitude   = cityResponse.Location.Longitude ?? double.NaN;
        }
        public GeoData()
        {

        }
        public Dictionary<string, string> Continent { get; set; }
        public Dictionary<string, string> Country   { get; set; }
        public Dictionary<string, string> City      { get; set; }
        public double Latitude                      { get; set; }
        public double Longitude                     { get; set; }
    }
    public class GeoDataLocal
    {
        public GeoDataLocal(GeoData geoData, string Language)
        {
            Latitude    = geoData.Latitude;
            Longitude   = geoData.Longitude;
            Continent   = geoData.Continent.GetValueOrDefault(Language);
            Country     = geoData.Country.GetValueOrDefault(Language);
            City        = geoData.City.GetValueOrDefault(Language);
        }
        public string Language  { get; set; }
        public string Continent { get; set; }
        public string Country   { get; set; }
        public string City      { get; set; }
        public double Latitude  { get; set; }
        public double Longitude { get; set; }
    }

}
