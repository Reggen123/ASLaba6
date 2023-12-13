using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLaba6
{
    public class ViewModel
    {
        private Parser parser;
        private int countofparsingobjects;
        public ViewModel()
        {
            parser = new Parser();
        }


        public void GetCountNumber()
        {
            bool Started = false;
            Console.WriteLine("Введите количество записываемых товаров");
            while (!Started)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out countofparsingobjects) && countofparsingobjects >= 0)
                {
                    Started = true;
                }
                else
                {
                    Console.WriteLine("Неверный ввод");
                }
            }
        }

        public void GetPersedObjects()
        {
            List<HeadphonesVM> listofheadphones = parser.Parse("naushniki-i-garnitury", countofparsingobjects);
            BdConnecting.InsertHeadphones(listofheadphones);
            Console.WriteLine("Полученные наушники:");
            foreach (var headphones in listofheadphones)
            {
                Console.WriteLine($"\nНазвание:{headphones.Name}\nЦена:{headphones.Price}\nБренд:{headphones.Brand}\nЦвет:{headphones.Color}\nСтрана:{headphones.CountryName}\nГарантия:{headphones.Guarantee}");
            }
        }

        public void WriteAbout()
        {
            Headphones h;
            Headphones l;
            string popularbrand;
            string popularcountry;
            using (var context = new HeadphonesContext())
            {
                int ph = (int)context.HeadPhones.Max(x => x.Price);
                int pl = (int)context.HeadPhones.Min(x => x.Price);
                h = context.HeadPhones.FirstOrDefault(x => x.Price == ph);
                l = context.HeadPhones.FirstOrDefault(x => x.Price == pl);

                int cid = (int)context.HeadPhones.Where(x => x.CountryId != null).GroupBy(x => x.CountryId).OrderByDescending(g => g.Count()).Select(g => g.Key).First();
                popularcountry = context.Countries.Find(cid).Name;
                popularbrand = context.HeadPhones.Where(x => x.Brand != null).GroupBy(x => x.Brand).OrderByDescending(g => g.Count()).Select(g => g.Key).First();
            }
            WordWriting wordWriting = new WordWriting(@"C:\DOC\DocR.doc");

            string hbrand = h.Brand == null ? "не указан" : h.Brand;
            string hcountry = h.Country == null || h.Country.Name == "None" ? "не указан" : h.Country.Name;
            wordWriting.Replace("{TelephoneHighCostName}", h.Name);
            wordWriting.Replace("{TelephoneHighCostPrice}", h.Price.ToString());
            wordWriting.Replace("{TelephoneHighCostBrand}", hbrand);
            wordWriting.Replace("{TelephoneHighCostCountry}", hcountry);
            wordWriting.ReplaceBookmark("HighCost", "Дорогие наушники\n");


            string lbrand = l.Brand == null ? "не указан" : l.Brand;
            string lcountry = l.Country == null || l.Country.Name == "None" ? "не указан" : l.Country.Name;
            wordWriting.Replace("{TelephoneLowCostName}", l.Name);
            wordWriting.Replace("{TelephoneLowCostPrice}", l.Price.ToString());
            wordWriting.Replace("{TelephoneLowCostBrand}", lbrand);
            wordWriting.Replace("{TelephoneLowCostCountry}", lcountry);
            wordWriting.ReplaceBookmark("LowCost", "Дешевые наушники\n");

            wordWriting.Replace("{PopularCountry}", popularcountry);
            wordWriting.Replace("{PopularBrand}", popularbrand);
            wordWriting.ReplaceBookmark("Facts", "Интересные заметки\n");

            wordWriting.Close();
            Console.WriteLine("Отчёт Word сохранён");
        }
    
        public void WriteExcel()
        {
            Dictionary<string, int> PricesCount = new Dictionary<string, int>();
            using (var context = new HeadphonesContext())
            {
                List<Headphones> headphones = context.HeadPhones.ToList();
                PricesCount.Add("0-1000", headphones.Where(x => x.Price >= 0 && x.Price < 1000).Count());
                PricesCount.Add("1000-3000", headphones.Where(x => x.Price >= 1000 && x.Price < 3000).Count());
                PricesCount.Add("3000-7000", headphones.Where(x => x.Price >= 3000 && x.Price < 7000).Count());
                PricesCount.Add("7000-12000", headphones.Where(x => x.Price >= 7000 && x.Price < 12000).Count());
                PricesCount.Add("12000-17000", headphones.Where(x => x.Price >= 12000 && x.Price < 17000).Count());
                PricesCount.Add("17000-250000", headphones.Where(x => x.Price >= 17000 && x.Price < 25000).Count());
                PricesCount.Add("25000-30000", headphones.Where(x => x.Price >= 25000 && x.Price < 30000).Count());
            }

            ExelWriting exel = new ExelWriting(@"C:\DOC\ExR9.xlsx");
            int i = 0;
            foreach (var item in PricesCount)
            {
                i++;
                exel.WriteExel(1, i, item.Value);
                exel.WriteExel(2, i, item.Key);
            }
            exel.WriteExelFormula(3, 1, $"=SUM({exel.RNGName(1, 1)}:{exel.RNGName(1, i)})");

            exel.WriteDiagramm(exel.RNGName(2, 1), exel.RNGName(2, i), exel.RNGName(1, 1), exel.RNGName(1, i), "Раскидка цен на наушники", "Количество");
            exel.Close();
            Console.WriteLine("Отчёт Exel сохранён");
        }
    
    
    }
}
