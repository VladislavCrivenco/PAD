using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PAD;
using System.Threading.Tasks;

namespace PAD
{
    public class HomeController : Controller
    {
        IRedisManager _iRedisManager;
        public HomeController(IRedisManager iRedisManager)
        {
            _iRedisManager = iRedisManager;
        }


        [Route("api/all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =  await _iRedisManager.GetConnection().GetAsync("api/all");
            

            return Json(new {result.Content.ReadAsStringAsync().Result});
        }
    }
}