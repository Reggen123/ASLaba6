using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLaba6
{
    public class HeadphonesContext: DbContext
    {
        public HeadphonesContext() : base("Server=localhost\\SQLEXPRESS; Database=DataBaseAS; Trusted_Connection=true;") { }
        public DbSet<Headphones> HeadPhones { get; set; }   
        public DbSet<Country> Countries { get; set; }
    }
}
