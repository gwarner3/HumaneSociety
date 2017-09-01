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
                case "4":
                    SetAdoptionStatus();
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
        public void SetAdoptionStatus()
        {
            var animals = from a in humaneSocietyData.Animals
                          select a;
            userResponse = menu.GetAnimalStatusToUpdate(animals.OrderBy(animal => animal.AdoptionStatus));
            var animalToUpdate = humaneSocietyData.Animals.Single(adoptStatus => adoptStatus.Animal_ID == Convert.ToInt32(userResponse));
            if (animalToUpdate.AdoptionStatus == "adopted")
            {
                animalToUpdate.AdoptionStatus = "not adopted";
            }
            else if (animalToUpdate.AdoptionStatus == "not adopted")
            {
                animalToUpdate.AdoptionStatus = "adopted";
            }
            humaneSocietyData.SubmitChanges();
            Console.WriteLine($"{animalToUpdate.Name} adoption status is now - {animalToUpdate.AdoptionStatus}");
            Console.ReadLine();
        }
        public void CalculateFoodNeeds()
        {
            userResponse = menu.GetUserFoodNeed();
            var animals = from a in humaneSocietyData.Animals
                          select a;
            var newAnimal = animals.ToList();
            newAnimal.ForEach((a) => { Console.WriteLine($"{a.Name} needs {a.Food.WeeklyServing * Convert.ToInt32(userResponse)} servings of {a.Food.Name} for {userResponse} weeks."); });
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
            userResponse = menu.DisplayAdopterMenu();
            switch (userResponse)
            {
                case "1":
                    CreateAdopterProfile();
                    break;
                case "2":
                    //AddAnimalToDatabase();
                    break;
                default:
                    Console.WriteLine("Wrong selection, try again. Press ENTER to continue");
                    Console.ReadLine();
                    RunAdopterFunctions();
                    break;
            }
        }
        public void CreateAdopterProfile()
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
                RunAdopterFunctions();
            }
            else
            {
                RunEmployeeFunctions();
            }

        }
    }
}
