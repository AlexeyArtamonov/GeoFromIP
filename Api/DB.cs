using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using MaxMind.GeoIP2;
namespace Api
{
    public class DB
    {
        private static DatabaseReader reader = null;
        public static Models.GeoData ReadFromDb(string ip)
        {
            if (reader == null)
                reader = new DatabaseReader(@"C:\Users\Alexey\source\repos\AlexeyArtamonov\GeoFromIP\Api\ipdb.mmdb");
            return new GeoData(reader.City(ip));    
        }
    }
}
