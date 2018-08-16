using Microsoft.AspNetCore.Mvc;
using System;

namespace Core2_0.Controllers
{
    [ApiVersion("1.0", Deprecated = true), Route("attr"), ApiVersion("2.0")]
    public class ValuesAttributeController : Controller
    {
        [HttpGet("{id:int?}/{*catchall}"), MapToApiVersion("1.0")]
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

        [HttpPost("actions/addorupdate/{type:bool?}"), MapToApiVersion("1.0")]
        public IActionResult AddOrUpdate()
        {
            var type = RouteData.Values["type"];
            if (type == null)
            {
                return BadRequest();
            }

            return Ok(Convert.ToBoolean(type) ? $"{nameof(AddOrUpdate)} - Add" : $"{nameof(AddOrUpdate)} - Update");
        }

        [HttpDelete("actions/delete"),]
        public IActionResult Delete()
        {
            return Ok(nameof(Delete));
        }
    }
}