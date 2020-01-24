using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mialco.shopping.connector.entities;
using mialco.shopping.entities.abstraction;
using mialco.shopping.connector.frontstore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mialco.shopping.connector.StoreFrontDbService.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        // GET: api/Product
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public Product Get(int id)
        {
			var p = new Product();
			//p.AssignId(22);
			//p.Description = "P33 Description";
			//p.Name = $"Product{id}";
			//p.LongDescription = $"Long Description {id}";

			return p;
        }
        
        // POST: api/Product
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
