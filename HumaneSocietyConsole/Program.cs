using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            HumaneSociety humaneSociety = new HumaneSociety(menu);
            humaneSociety.OpenHumaneSociety();
        }
    }
}
