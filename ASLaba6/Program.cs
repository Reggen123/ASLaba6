using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLaba6
{
    class Program
    {
        static void Main(string[] args)
        {
            ViewModel VM = new ViewModel();
            VM.GetCountNumber();
            VM.GetPersedObjects();

            VM.WriteAbout();
            VM.WriteExcel();
            Console.ReadKey();
        }
    }
}
