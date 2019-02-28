using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OverApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        [HttpGet, Route("Get")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllHeroesAsync()
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

        [HttpGet, Route("Get/{hero}")]
        public async Task<IActionResult> GetHero([FromRoute]string hero)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = $"https://playoverwatch.com/fr-fr/heroes/{hero}/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            if (document.StatusCode != HttpStatusCode.OK) return NotFound();
            using (document)
            {

                var cells = document.QuerySelectorAll("span.hero-bio-copy");
                var x = cells.Select(c => c.TextContent);

                return Ok(x);
            }
        }
    }
}