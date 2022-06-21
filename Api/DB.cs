using Api.Models;
using MaxMind.GeoIP2;
using System.Net;

namespace Api
{
    public class DB
    {
        private static DatabaseReader reader = null;
        public static Models.GeoData ReadFromDb(string ip)
        {
            if (IPAddress.TryParse(ip, out System.Net.IPAddress address) == false)
                return null;

            if (reader == null)
                reader = new DatabaseReader("ipdb.mmdb");

            try
            {
               return new GeoData(reader.City(address));    
            }
            catch
            {
                return null;
            }
        }
    }
}
