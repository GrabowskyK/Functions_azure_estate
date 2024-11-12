using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            HttpClient client = new HttpClient();

            // Pobranie zawartoœci strony
            string url = "https://www.otodom.pl/pl/wyniki/sprzedaz/mieszkanie/slaskie/tychy/tychy/tychy?viewType=listing";
            //string url = "https://webscraper.io/test-sites/e-commerce/allinone";
            HttpResponseMessage response = await client.GetAsync(url);

            // Sprawdzenie, czy ¿¹danie zakoñczy³o siê sukcesem
            if (response.IsSuccessStatusCode)
            {
                // Odczytanie treœci odpowiedzi (kod HTML)
                string html = await response.Content.ReadAsStringAsync();
                File.WriteAllTextAsync("./file.txt", html);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                Console.WriteLine(html);
                // Wyszukiwanie elementów <p class="description card-text">
           //     var h4Elements = htmlDoc.DocumentNode.SelectNodes("//h4[a]");
                //var reviews = htmlDoc.DocumentNode.Descendants("p").Where(p => p.Attributes["class"].Value.Contains("review-count"));
               // var divReviws = htmlDoc.DocumentNode.SelectNodes("//div[@class='ratings']");

                //if (h4Elements != null)
                //{
                //    foreach (var h4 in h4Elements)
                //    {
                //        // Znalezienie <a> w <h4>
                //        var aTag = h4.SelectSingleNode(".//a");

                //        if (aTag != null)
                //        {
                //            // Pobranie wartoœci href i title z <a>
                //            string href = aTag.GetAttributeValue("href", string.Empty);
                //            string title = aTag.GetAttributeValue("title", string.Empty);

                //            Console.WriteLine($"Href: {href}, Title: {title}");
                //            _logger.LogInformation($"Href: {href}, Title: {title}");
                //        }
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("Nie znaleziono ¿adnych <h4> z <a>.");
                //}
                //foreach (var div in divReviws)
                //{
                // //   Console.WriteLine(div);
                //    var reviewsCount = div.SelectSingleNode("//p[@class='review-count float-end']");
                //    Console.WriteLine(reviewsCount.InnerText.Trim());
                //    var rating = div.SelectSingleNode("//p[@data-rating]").GetAttributeValue("data-rating", "Brak oceny");
                //    Console.WriteLine(rating);
                //}
            }
        }
    }
}
