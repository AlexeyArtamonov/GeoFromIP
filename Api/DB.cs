using Api.Models;
using MaxMind.GeoIP2;
using System.Net;

namespace Api
{
    public class DB
    {
        private static DatabaseReader reader = null;
        private static string Path = System.IO.File.ReadAllText("Config.cfg").Remove(0, 5);
        public static Models.GeoData ReadFromDb(string ip)
        {
            if (IPAddress.TryParse(ip, out System.Net.IPAddress address) == false)
                return null;

            if (reader == null)
                reader = new DatabaseReader(Path + "ipdb.mmdb");

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
