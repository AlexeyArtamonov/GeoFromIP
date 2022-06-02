using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GeoDataController : ControllerBase
    {
        GeoDataContext db;
        static readonly HttpClient client = new HttpClient();
        public GeoDataController(GeoDataContext context)
        {
            db = context;
            if (!db.GeoDatas.Any())
            {
                db.GeoDatas.Add(new GeoData { ip = "0.0.0.0", city = "Moscow" });
            }
            db.SaveChanges();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoData>>> Get()
        {
            return await db.GeoDatas.ToListAsync();
        }
        [HttpGet("{ip}")]
        public async Task<ActionResult<string>> Get(string ip)
        {
            GeoData data = await db.GeoDatas.FirstOrDefaultAsync(x => x.ip == ip);
            if (data == null)
            {
                HttpResponseMessage response = await client.GetAsync($"http://api.ipstack.com/{ip}?access_key=0f0eb71f3dd66dd3f4d963aea73a269f");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                try
                {
                    data = JsonSerializer.Deserialize<GeoData>(responseBody);
                }
                catch
                {
                    return NotFound();
                }
                if (data == null)
                {
                    return NotFound();
                }

                db.GeoDatas.Add(data);
                db.SaveChanges();
                
            }
            return Ok(new ObjectResult(data)) ;
        }
    }
}
