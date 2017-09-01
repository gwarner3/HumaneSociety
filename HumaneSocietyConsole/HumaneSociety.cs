using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    class HumaneSociety
    {
        private string userResponse;
        private HumaneSocietyDataContext humaneSocietyData = new HumaneSocietyDataContext();

        Menu menu;
        public HumaneSociety(Menu menu)
        {
            this.menu = menu;
        }
        public void RunEmployeeFunctions()
        {
            userResponse = menu.DisplayEmployeeMenu();
            switch (userResponse)
            {
                case "1":
                    ListAnimalCategories();
                    break;
                case "2":
                    //AddAnimalToDatabase();
                    break;
                case "3":
                    ListAnimalRoomNumnbers();                    
                    break;
                case "7":
                    CalculateFoodNeeds();
                        break;
                default:
                    Console.WriteLine("Wrong selection, try again. Press ENTER to continue");
                    Console.ReadLine();
                    RunEmployeeFunctions();
                    break;
            }
        }
        public void CalculateFoodNeeds()
        {
            userResponse = menu.GetUserFoodNeed();
            var animals = from a in humaneSocietyData.Animals
                          select a;
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"{animal.Name} needs {animal.Food.WeeklyServing * Convert.ToInt32(userResponse)} servings of {animal.Food.Name} for {userResponse} weeks.");
            }
            Console.ReadLine();
        }
        public void ListAnimalCategories()
        {
            var animals = from a in humaneSocietyData.Animals
                          select a;
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"{animal.Name} is a {animal.AnimalType.TypeName}");
            }
            Console.Read();
        }
        public void ListAnimalRoomNumnbers()
        {
            var animals = from a in humaneSocietyData.Animals
                        select a;
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"{animal.Name} is in {animal.Room.Number}");
            }
        }
        public void RunAdopterFunctions()
        {

        }
        public void AddAnimalToDatabase()
        {

        }
        
        public void OpenHumaneSociety()
        {
            userResponse = menu.EmplyeeOrAdopter();

            if (userResponse == "1")
            {
                RunEmployeeFunctions();                
            }
            else
            {
                RunAdopterFunctions();
            }

        }
    }
}
