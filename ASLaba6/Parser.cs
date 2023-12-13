using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASLaba6
{
    public class Parser
    {
        const string siteUrl = "https://2droida.ru/collection/";
        ChromeOptions options;

        public Parser()
        {
            options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--ignore-certificate-errors-spki-list");

        }

        public List<HeadphonesVM> Parse(string relationalUrl, int count)
        {
            if(count < 0)
            {
                return null;
            }
            List<HeadphonesVM> output = new List<HeadphonesVM>();
            if (count == 0)
            {
                return output;
            }
            var driver = new ChromeDriver(options);
            if (relationalUrl.Contains("https://2droida.ru"))
            {
                driver.Navigate().GoToUrl(relationalUrl);
            }
            else
            {
                driver.Navigate().GoToUrl(siteUrl + relationalUrl);
            }

            var products = driver.FindElements(By.ClassName("product-preview__content"));
            Dictionary<HeadphonesVM, string> productslinks = new Dictionary<HeadphonesVM, string>();
            //var titles = driver.FindElements(By.ClassName("product-preview__title"));
            //var prices = driver.FindElements(By.ClassName("product-preview__price-cur"));


            //for (int i = 0; i < titles.Count; i++)
            //{
            //    Headphones lap = new Headphones { Name = titles[i].Text, Price = int.Parse(Regex.Replace(prices[i].Text, @"\D", "")) };
            //    output.Add(lap);
            //}

            for (int i = 0; i < products.Count && i < count; i++)
            {
                var title = products[i].FindElement(By.ClassName("product-preview__title"));
                var price = products[i].FindElement(By.ClassName("product-preview__price-cur"));
                int? Price = null;
                if(price != null)
                {
                    Price = int.Parse(Regex.Replace(price.Text, @"\D", ""));
                }
                HeadphonesVM lap = new HeadphonesVM { Name = title.Text, Price = Price };
                productslinks.Add(lap,title.FindElement(By.TagName("a")).GetAttribute("href"));
                output.Add(lap);
            }

            foreach (var item in productslinks.Keys)
            {
                driver.Navigate().GoToUrl(productslinks[item]);
                var stats = driver.FindElements(By.ClassName("property"));
                foreach (var stat in stats)
                {
                    if(stat.FindElement(By.ClassName("property__name")).Text == "Цвет")
                        item.Color = stat.FindElement(By.ClassName("property__content")).Text;
                    if (stat.FindElement(By.ClassName("property__name")).Text == "Бренд")
                        item.Brand = stat.FindElement(By.ClassName("property__content")).Text;
                    if (stat.FindElement(By.ClassName("property__name")).Text == "Страна производитель")
                        item.CountryName = stat.FindElement(By.ClassName("property__content")).Text;
                    if (stat.FindElement(By.ClassName("property__name")).Text == "Гарантия")
                        item.Guarantee = stat.FindElement(By.ClassName("property__content")).Text;
                }
            }

            //driver.Navigate().GoToUrl(titles[0].FindElement(By.TagName("a")).GetAttribute("href"));
            //var stats = driver.FindElements(By.ClassName("property__name"));

            driver.Quit();
            return output;
        }
    }
}
