using System;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_2.Controllers
{
    [RoutePrefix("attr")]
    public class ValuesAttributeController : ApiController
    {
        [HttpGet, Route("{id:int?}/{*catchall}")]
        public IHttpActionResult Get(int? id = null)
        {
            var routeData = Request.GetRouteData();

            return Ok(new
            {
                Controller = nameof(ValuesAttributeController),
                Action = nameof(Get),
                Id = id,
                AnotherParams = routeData.Values["catchAll"]
            });
        }

        [HttpGet, Route("actions/addorupdate/{type:bool}")]
        public IHttpActionResult AddOrUpdate()
        {
            var routeData = Request.GetRouteData();
            var type = routeData.Values["type"];

            if (type == null)
            {
                return BadRequest();
            }

            return Ok(Convert.ToBoolean(type) ? $"{nameof(AddOrUpdate)} - Add" : $"{nameof(AddOrUpdate)} - Update");
        }

        [HttpDelete, Route("actions/delete")]
        public IHttpActionResult Delete()
        {
            return Ok(nameof(Delete));
        }
    }
}
