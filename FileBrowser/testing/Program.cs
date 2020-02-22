using FileBrowser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> VoidDir = Enum.GetNames(typeof(Environment.SpecialFolder)).ToList<string>();
            foreach(string dir in VoidDir)
            {
                Console.WriteLine(dir);
            }
        }
    }
}
