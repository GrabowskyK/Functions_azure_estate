using Azure;
using FunctionApp1.model;
using Google.Protobuf;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace FunctionApp1
{
    public class otodom
    {
        private readonly ILogger<otodom> _logger;

        public otodom(ILogger<otodom> logger)
        {
            _logger = logger;
            
        }

        [Function("otodom")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            List<FlatDetails> details = new List<FlatDetails>();

            HtmlDocument doc;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = new HtmlDocument();
            string text = "";
            int x = 1, y = 1;
            string pages = "";
            web.OverrideEncoding = Encoding.UTF8;
            web.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0";
            string page = "";
            do
            {
                Console.WriteLine($"");
                Console.WriteLine($"==============PAGE {y}=======================");
                doc = web.Load($"https://www.otodom.pl/pl/wyniki/sprzedaz/mieszkanie/slaskie/tychy/tychy/tychy?viewType=listing{page}");
                string html = doc.DocumentNode.OuterHtml;

                htmlDoc.LoadHtml(html);
                pages = htmlDoc.DocumentNode.SelectSingleNode("//li[@class='css-43nhzf']").InnerHtml;
                var divs = htmlDoc.DocumentNode.SelectNodes("//div[@class='css-13gthep eeungyz2']");

                foreach (var div in divs)
                {
                    List<string> test = new List<string>();
                    FlatDetails NewFlat = new FlatDetails();

                    text = "";
                    var price = div.SelectSingleNode(".//span[@direction='horizontal']").InnerText;
                    NewFlat = Parsers.ParsePriceAndCurrency(price, NewFlat);

                    var place = div.SelectSingleNode(".//div[@class='css-12h460e e5ogpj51']").InnerText;
                    NewFlat.Address = Parsers.ParseAddress(place);

                    var basicData = div.SelectSingleNode(".//dl[@class='css-12dsp7a e1clni9t1']").SelectNodes(".//dd");
                    foreach (var data in basicData)
                    {
                        string[] InnerData = data.InnerText.Split(" ");
                        test.Add(data.ToString());
                        text += data.InnerText.Trim() + ", ";
                    }
                    NewFlat.Rooms = test[0].ToString();
                    NewFlat.Area = test[1].ToString();
                    NewFlat.Floor = test[test.Count - 1].ToString();
                    details.Add(NewFlat);
                    x++;
                }
                y++;
                page = $"&page={y.ToString()}";

                //} while (y <= Int32.Parse(pages));
            } while (y <= 3);
        
        var groupedDetailsCount = details.GroupBy(detail => detail.Address.Estate)
                                             .Select(group => new { Estate = group.Key, Count = group.Count() })
                                             .ToList();

            var avgPrice = details.GroupBy(detail => detail.Address.Estate)
                .Select(group => new
                {
                    Estate = group.Key,
                    avgPrice = group.Average(x => x.Price),
                    minPrice = group.Min(x => (x.Price)),
                    maxPrice = group.Max(x => (x.Price))
                });

            foreach (var group in avgPrice)
            {
                Console.WriteLine($"Estate: {group.Estate}, Avg Price: {group.avgPrice}, Min Price: {group.minPrice}, Max Price: {group.maxPrice}");
            }
        }
    }
}
