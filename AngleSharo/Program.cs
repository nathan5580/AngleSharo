using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

namespace AngleSharo
{
    class Program
    {
        static void Main(string[] args)
        {
            DoSomething();

            Console.ReadLine();
        }

        public static async void DoSomething()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://playoverwatch.com/fr-fr/heroes/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var cellSelector = "span.portrait-title";
            var cells = document.QuerySelectorAll(cellSelector);
            var titles = cells.Select(m => m.TextContent);

            titles.ToList().ForEach(i => Console.WriteLine(i));
        }
    }
}
