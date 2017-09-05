using System;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    public class Menu
    {
        private string userResponse;

        public int SearchByCaegory(Table<AnimalType> types)
        {
            Console.WriteLine("Key in category ID of animal to search");
            foreach (AnimalType type in types)
            {
                Console.WriteLine($"{type.AnimalType_ID} {type.TypeName}");
            }
            userResponse = Console.ReadLine();
            return Convert.ToInt32(userResponse);
        }
        public decimal PromptForMaxPrice()
        {
            Console.WriteLine("Key in the max price and press ENTER");
            userResponse = Console.ReadLine();
            return Convert.ToDecimal(userResponse);
        }
        public decimal PromptForMinPrice()
        {
            Console.WriteLine("Key in the minimum price and press ENTER");
            userResponse = Console.ReadLine();
            return Convert.ToDecimal(userResponse);
        }
        public decimal GetPayment(Table<Animal> animals)
        {
            Console.WriteLine("Key in amount of payment and press ENTER");
            userResponse = Console.ReadLine();
            return Convert.ToDecimal(userResponse);
        }
        public int SelectAdopter(Table<Adopter> adopters)
        {
            Console.WriteLine("Key in the ID of the adopter and press ENTER");
            foreach (Adopter adopter in adopters)
            {
                Console.WriteLine($"{adopter.Adopter_ID}. {adopter.FirstName} {adopter.LastName}");
            }
            userResponse = Console.ReadLine();
            return Convert.ToInt32(userResponse);
        }
        public int ShowShotStatus(Table<Animal> animals)
        {
            Console.WriteLine("Key in ID of the animal and press ENTER\nAnimal ID\tShot Satus\n");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"{animal.Animal_ID}\t{animal.HasShoots}\t{animal.Name}\t{animal.AnimalType.TypeName}");
            }
            userResponse = Console.ReadLine();
            return Convert.ToInt32(userResponse);
        }
        public int SelectAnimal(Table<Animal> animals)
        {
            Console.WriteLine("Key in ID of the animal and press ENTER\nAnimal ID\n");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"{animal.Animal_ID}\t{animal.Name}\t{animal.AnimalType.TypeName}\t\t{animal.Price}");
            }
            userResponse = Console.ReadLine();
            return Convert.ToInt32(userResponse);
        }
        public string EmplyeeOrAdopter()
        {
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
            Console.WriteLine("1. Display animals by category\n2. Add animal to database\n3. List animal room numbers\n4. Set animal adoption status\n5. Collect payment\n6. Update animal shot status\n7. Calculate food serving needs");
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
        public string PromptForAnimalName()
        {
            Console.WriteLine("Key in animals name and press ENTER");
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
        public decimal PromptForAnimalPrice()
        {
            Console.WriteLine("Key in animals price and press ENTER");
            userResponse = Console.ReadLine();
            return Convert.ToDecimal(userResponse);
        }
        public string SetAnimalAdoptionStatus()
        {
            Console.WriteLine("Key in number of adoption status and press ENTER.\n1. Adopted\n2. Not Adopted");
            userResponse = Console.ReadLine();
            switch (userResponse)
            {
                case "1":
                    userResponse = "adopted";
                    break;
                case "2":
                    userResponse = "not adopted";
                    break;
                default:
                    Console.WriteLine("Please select one of the three options. Press ENTER to continue");
                    Console.ReadLine();
                    SetAnimalAdoptionStatus();
                    break;
            }
            return userResponse;
        }
        public int SetAnimalType(Table<AnimalType> animalTypes)
        {
            Console.WriteLine("Key in the number of the animal type and press ENTER");
            foreach (AnimalType type in animalTypes)
            {
                Console.WriteLine($"{type.AnimalType_ID}. {type.TypeName}");
            }
            userResponse = Console.ReadLine();
            //validate user input
            //int test = Convert.ToInt32(userResponse);
            //if (animalTypes.Any(type => type.AnimalType_ID != test))
            //{
            //    Console.WriteLine("Please key in a valid ID form the list");
            //    SetAnimalType(animalTypes);
            //}
            return Convert.ToInt32(userResponse);

        }
        public int SetAnimalFoodType(Table<Food> animalFoods)
        {
            Console.WriteLine("Key in the number of the food and press ENTER.");
            foreach (Food food in animalFoods)
            {
                Console.WriteLine($"{food.Food_ID}. {food.Name}");
            }
            //input validation here
            userResponse = Console.ReadLine();
            return Convert.ToInt32(userResponse);
        }
        public int SetRoomNumber(Table<Room> rooms)
        {
            Console.WriteLine("Key in the ID# of the room and press ENTER.");
            foreach (Room room in rooms)
            {
                Console.WriteLine($"{room.Room_ID}. {room.Number}");
            }
            return Convert.ToInt32(userResponse);
        }
        public string SetAnimalShotStatus()
        {
            Console.WriteLine("Key in number of status and press ENTER.\n1. Shots given.\n2. Shots needed.\n3. Shots not required");
            userResponse = Console.ReadLine();
            switch (userResponse)
            {
                case "1":
                    userResponse = "yes";
                    break;
                case "2":
                    userResponse = "no";
                    break;
                case "3":
                    userResponse = "not required";
                    break;
                default:
                    Console.WriteLine("Please select one of the three options. Press ENTER to continue");
                    Console.ReadLine();
                    SetAnimalShotStatus();
                    break;
            }
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
            Console.WriteLine("Key in the gender number and press ENTER\n1. Male\n2. Female\n3. Gender not listed");
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
