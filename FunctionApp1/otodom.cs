using Azure;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;

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
                    text = "";
                    var price = div.SelectSingleNode(".//span[@direction='horizontal']").InnerText;
                    var place = div.SelectSingleNode(".//div[@class='css-12h460e e5ogpj51']").InnerText;
                    var basicData = div.SelectSingleNode(".//dl[@class='css-12dsp7a e1clni9t1']").SelectNodes(".//dd");
                    //foreach (var data in basicData)
                    //    text += data.InnerHtml.Trim() + ", ";

                    Console.WriteLine($"{x.ToString()}. {price} - {place} ");
                    //Console.WriteLine($"{text}");
                    Console.WriteLine($"");
                    x++;
                }
                y++;
                page = $"&page={y.ToString()}";

            //} while (y <= Int32.Parse(pages));
            } while (y <= 16);
          //  doc = web.Load("https://www.otodom.pl/pl/wyniki/sprzedaz/mieszkanie/slaskie/tychy/tychy/tychy?viewType=listing");
           // string html = doc.DocumentNode.OuterHtml;

            //var htmlDoc = new HtmlDocument();
            //htmlDoc.LoadHtml(html);
            //var pages = htmlDoc.DocumentNode.SelectSingleNode("//li[@class='css-43nhzf']").InnerHtml;
            //var divs = htmlDoc.DocumentNode.SelectNodes("//div[@class='css-13gthep eeungyz2']");
            //int x = 1;
            //string text = "";
            //foreach (var div in divs)
            //{
            //    text = "";
            //    var price = div.SelectSingleNode(".//span[@direction='horizontal']").InnerText;
            //    var place = div.SelectSingleNode(".//div[@class='css-12h460e e5ogpj51']").InnerText;
            //    var basicData = div.SelectSingleNode(".//dl[@class='css-12dsp7a e1clni9t1']").SelectNodes(".//dd");
            //    //foreach (var data in basicData)
            //    //    text += data.InnerHtml.Trim() + ", ";

            //    Console.WriteLine($"{x.ToString()}. {price} - {place} ");
            //    //Console.WriteLine($"{text}");
            //    Console.WriteLine($"");
            //    x++;
            //}
        }
    }
}
