using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLaba6
{
    public class HeadphonesVM
    {
        public string Name { get; set; }
        public Nullable<int> Price { get; set; }
        public System.Guid ID { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Guarantee { get; set; }
        public string CountryName { get; set; }
    }
}
