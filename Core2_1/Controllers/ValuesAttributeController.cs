using System;
using Microsoft.AspNetCore.Mvc;

namespace Core2_1.Controllers
{
    [Route("attr")]
    public class ValuesAttributeController : Controller
    {
        [HttpGet("{id:int?}/{*catchall}")]
        public IActionResult Get(int? id)
        {
            return Ok(new
            {
                Controller = nameof(ValuesAttributeController),
                Action = nameof(Get),
                Id = id,
                AnotherParams = RouteData.Values["catchall"]
            });
        }

        [HttpPost("actions/addorupdate/{type:bool?}")]
        public IActionResult AddOrUpdate()
        {
            var type = RouteData.Values["type"];
            if (type == null) return BadRequest();

            return Ok(Convert.ToBoolean(type) ? $"{nameof(AddOrUpdate)} - Add" : $"{nameof(AddOrUpdate)} - Update");
        }

        [HttpDelete("actions/delete")]
        public IActionResult Delete()
        {
            return Ok(nameof(Delete));
        }
    }
}