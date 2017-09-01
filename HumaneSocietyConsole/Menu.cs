using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    public static class Menu
    {
        private static string userResponse;

        public static void EmplyeeOrAdopter()
        {
            Console.WriteLine("Enter as:\n1. User\n2. Adopter");
            userResponse = Console.ReadLine();
            ChooseMenu(userResponse);
        }
        public static void ChooseMenu(string userResponse)
        {

        }
    }
}
