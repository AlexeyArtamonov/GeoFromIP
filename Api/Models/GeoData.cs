using MaxMind.GeoIP2;
using System.Collections.Generic;
using System.Linq;

namespace Api.Models
{
    public class GeoData
    {
        public GeoData(MaxMind.GeoIP2.Responses.CityResponse cityResponse)
        {
            Continent   = cityResponse.Continent.Names.ToDictionary(pair => pair.Key, pair => pair.Value);
            Country     = cityResponse.Country.Names.ToDictionary(pair => pair.Key, pair => pair.Value);
            City        = cityResponse.City.Names.ToDictionary(pair => pair.Key, pair => pair.Value);
            Latitude    = cityResponse.Location.Latitude ?? double.NaN;
            Longitude   = cityResponse.Location.Longitude ?? double.NaN;
        }
        public GeoData()
        {

        }
        public Dictionary<string, string> Continent;
        public Dictionary<string, string> Country;
        public Dictionary<string, string> City;
        public double Latitude;
        public double Longitude;
    }

}
