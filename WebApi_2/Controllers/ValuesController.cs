using System;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_2.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int? id)
        {
            var routeData = Request.GetRouteData();

            return Ok(new
            {
                Controller = nameof(ValuesController),
                Action = nameof(Get),
                Id = id,
                AnotherParams = routeData.Values["catchAll"]
            });
        }

        [HttpPost]
        public IHttpActionResult AddOrUpdate()
        {
            var routeData = Request.GetRouteData();
            var type = routeData.Values["type"];

            if (type == null) return BadRequest();

            return Ok(Convert.ToBoolean(type) ? $"{nameof(AddOrUpdate)} - Add" : $"{nameof(AddOrUpdate)} - Update");
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return Ok(nameof(Delete));
        }
    }
}
