using System;
using Microsoft.AspNetCore.Mvc;

namespace Core2_0.Controllers.V2
{
    [ApiVersion("2.0"), Route("attr")]
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
                Guid = Guid.NewGuid(),
                AnotherParams = RouteData.Values["catchall"]
            });
        }

        [HttpPost("actions/addorupdate/{id}/{type:bool?}")]
        public IActionResult AddOrUpdate(Guid id)
        {
            var type = RouteData.Values["type"];
            if (type == null) return BadRequest();

            return Ok(Convert.ToBoolean(type) ? $"{nameof(AddOrUpdate)} - Add" : $"{nameof(AddOrUpdate)} - Update");
        }
    }
}