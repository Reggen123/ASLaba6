using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLaba6
{
    public class BdConnecting
    {
        public static void InsertHeadphones(List<HeadphonesVM> headphones)
        {
            using (var context = new HeadphonesContext())
            {
                foreach (var item in headphones)
                {
                    Headphones headphone = new Headphones();
                    headphone.ID = Guid.NewGuid();
                    headphone.Name = item.Name;
                    headphone.Price = item.Price;
                    headphone.Color = item.Color;
                    headphone.Brand = item.Brand;
                    headphone.Guarantee = item.Guarantee;
                    item.CountryName = item.CountryName == null ? "None" : item.CountryName;
                    Country country = context.Countries.First(a => a.Name == item.CountryName);
                    headphone.Country = country;
                    headphone.CountryId = country.ID;
                    context.HeadPhones.Add(headphone);
                }
                context.SaveChanges();
            }
        }
    }
}
