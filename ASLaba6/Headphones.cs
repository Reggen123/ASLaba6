using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLaba6
{
    public partial class Headphones
    {
        public string Name { get; set; }
        public Nullable<int> Price { get; set; }
        public System.Guid ID { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Guarantee { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Country Country { get; set; }

    }
}
