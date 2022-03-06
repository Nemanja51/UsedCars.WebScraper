using System.Configuration;

var brand = ConfigurationManager.AppSettings.Get("brand");
var model = ConfigurationManager.AppSettings.Get("model");
int yearFrom = Convert.ToInt32(ConfigurationManager.AppSettings.Get("yearFrom"));
int yearTo = Convert.ToInt32(ConfigurationManager.AppSettings.Get("yearTo"));
int countFromConfig = Convert.ToInt32(ConfigurationManager.AppSettings.Get("carsForSaleSum"));

string html = UsedCars.WebScraper.UsedCars.GetUsedCarsHtml(brand, model, yearFrom, yearTo);
string countText = UsedCars.WebScraper.UsedCars.ParseHtml(html);
int count = UsedCars.WebScraper.UsedCars.CarsCount(countText);

if (count!=countFromConfig)
{
    var diff = count - countFromConfig;
    EmailSender.EmailSender.SendEmail(diff);

    ConfigurationManager.AppSettings.Set("carsForSaleSum", count.ToString());
}
