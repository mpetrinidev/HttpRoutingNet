using Microsoft.AspNetCore.Mvc;
using System;

namespace Core2_0.Controllers
{
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get(int? id)
        {
            return Ok(new
            {
                Controller = nameof(ValuesController),
                Action = nameof(Get),
                Id = id,
                AnotherParams = RouteData.Values["catchall"]
            });
        }
        
        [HttpPost]
        public IActionResult AddOrUpdate()
        {
            var type = RouteData.Values["type"];
            if (type == null) return BadRequest();

            return Ok(Convert.ToBoolean(type) ? $"{nameof(AddOrUpdate)} - Add" : $"{nameof(AddOrUpdate)} - Update");
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok(nameof(Delete));
        }
    }
}