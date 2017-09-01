using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    public class Menu
    {
        private string userResponse;

        public string EmplyeeOrAdopter()
        {
            Console.Clear();
            Console.WriteLine("Enter as:\n1. Adopter\n2. Employee");
            userResponse = Console.ReadLine();
            return userResponse;
        }
        public string GetUserFoodNeed()
        {
            Console.WriteLine("Key in the number of weeks you need to calculate food for and press ENTER.");
            userResponse = Console.ReadLine();
            return userResponse;
        }
        public string DisplayEmployeeMenu()
        {
            Console.WriteLine("1. Display animals by category\n2. Add animal to database\n3. List animal room numbers\n4. Set animal adoption status\n5. Get payment\n6. List animal shot status\n7. Calculate food serving needs");
            userResponse = Console.ReadLine();
            return userResponse;            
        }
        public string GetAnimalStatusToUpdate(IQueryable<Animal> animals)
        {
            Console.WriteLine("Type an animals numeric ID and press ENTER to change that animals status.");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"{animal.Animal_ID} - {animal.Name} is {animal.AdoptionStatus}");
            }
            userResponse = Console.ReadLine();
            return userResponse;
        }
        public string PromptForFirstName()
        {
            Console.WriteLine("Key in your first name and press ENTER");
            userResponse = Console.ReadLine();
            return userResponse;
        }
        public string PromptForLastName()
        {
            Console.WriteLine("Key in your last name and press ENTER");
            string userResponse = Console.ReadLine();
            return userResponse;
        }  
        public DateTime PromptForDOB()
        {
            Console.WriteLine("Key in DOB as YYYY/MM/DD");
            userResponse = Console.ReadLine();
            return Convert.ToDateTime(userResponse);
        }
        public int PromptForAnnualIncome()
        {
            Console.WriteLine("Key in your annual income and press ENTER");
            userResponse = Console.ReadLine();
            return Convert.ToInt32(userResponse);
        }
        public string PromptForBio()
        {
            Console.WriteLine("Type up a short biography and press ENTER");
            string userResponse = Console.ReadLine();
            return userResponse;
        }     
        public int PromptForGender()
        {
            Console.WriteLine("Key in the number for your gender and press ENTER\n1. Male\n2. Female\n3. Other");
            userResponse = Console.ReadLine();
            return Convert.ToInt32(userResponse);
        }
        public string DisplayAdopterMenu()
        {
            Console.WriteLine("1. Create a profile\n2. Search for animals");
            userResponse = Console.ReadLine();
            return userResponse;
        }
    }
}
