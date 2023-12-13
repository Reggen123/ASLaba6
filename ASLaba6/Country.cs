using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLaba6
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Headphones> Headphones { get; set; }

        public Country()
        {
            Headphones = new List<Headphones>();
        }
    }
}
