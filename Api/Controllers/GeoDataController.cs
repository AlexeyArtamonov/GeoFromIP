using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("text/plain")]
    public class GeoDataController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get(string ip, string language)
        {
            GeoData geoData = DB.ReadFromDb(ip);

            if (geoData == null)
                return NotFound();

            if (language == null)
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(geoData, Newtonsoft.Json.Formatting.Indented));

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(new GeoDataLocal(geoData, language), Newtonsoft.Json.Formatting.Indented));
        }
    }
}
