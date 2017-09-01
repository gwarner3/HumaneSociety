﻿using System;
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
            Console.WriteLine("Enter as:\n1. Employee\n2. Adopter");
            userResponse = Console.ReadLine();
            switch (userResponse)
            { 
                case "1":
                    DisplayAdopterMenu();
                    break;
                case "2":
                    DisplayEmployeeMenu();
                    break;
                default:
                    Console.WriteLine("Wrong selection, try again. Press ENTER to continue");
                    Console.ReadLine();
                    EmplyeeOrAdopter();
                    break;
            }
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
            Console.WriteLine("Type an animals name and press ENTER to change that animals status.");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"{animal.Name} is {}");
            }
            userResponse = Console.ReadLine();
            return userResponse;
        }
        public  void DisplayAdopterMenu()
        {
        }
    }
}
