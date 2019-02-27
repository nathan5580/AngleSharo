using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OverApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        [HttpGet,Route("Get")]
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
    }
}