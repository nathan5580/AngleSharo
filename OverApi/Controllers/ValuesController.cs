using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Mvc;

namespace OverApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://playoverwatch.com/fr-fr/heroes/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            

            using (document)
            {
                var cellSelector = "span.portrait-title";
                var cells = document.QuerySelectorAll(cellSelector);
                var titles = cells.Select(m => m.TextContent);

                return titles.ToList();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
