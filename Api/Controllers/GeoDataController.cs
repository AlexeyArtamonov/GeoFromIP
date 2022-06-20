using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("text/plain")]
    public class GeoDataController : ControllerBase
    {
        public GeoDataController()
        {
          
        }
        [HttpGet("{ip}")]
        public async Task<ActionResult<string>> Get(string ip)
        {
            GeoData geoData = DB.ReadFromDb(ip);

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(geoData, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
