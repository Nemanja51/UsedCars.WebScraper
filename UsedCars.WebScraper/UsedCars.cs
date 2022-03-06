using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsedCars.WebScraper
{
    public static class UsedCars
    {
        public static string GetUsedCarsHtml(string brand, string model, int yearFrom, int yearTo)
        {
            string urlWithParameters = $"https://www.polovniautomobili.com/auto-oglasi/pretraga?brand={brand}&model%5B%5D={model}&price_to=&year_from={yearFrom}&year_to={yearTo}&showOldNew=all&submit_1=&without_price=1";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(urlWithParameters);

            return html.Result;
        }
        public static string ParseHtml(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var carsForSaleTotalParent = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.HasClass("js-hide-on-filter")).ToList();
            var carsForSaleTotal = carsForSaleTotalParent[1].Descendants("small").Where(c => c.InnerText.Contains("Prikazano od 1 do 25 oglasa od ukupno")).FirstOrDefault();

            return carsForSaleTotal.InnerText;
        }
        public static int CarsCount(string text) 
        {
            int startIndex = text.LastIndexOf(' ');
            int endIndex = text.Length-startIndex;

            var count = text.Substring(startIndex, endIndex).TrimStart().TrimEnd();

            return Convert.ToInt32(count);  
        }
    }
}
