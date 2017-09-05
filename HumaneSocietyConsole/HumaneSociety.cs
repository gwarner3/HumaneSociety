﻿using System;
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
                    AddAnimalToDatabase();
                    break;
                case "3":
                    ListAnimalRoomNumnbers();                    
                    break;
                case "4":
                    SetAdoptionStatus();
                    break;
                case "5":
                    GetPayment();
                    break;
                case "6":
                    UpdateAnimalShotStatus();
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
        public void UpdateAnimalShotStatus()
        {
            Animal animalSelected = new Animal();            
            int animalSelectedID = menu.ShowShotStatus(humaneSocietyData.Animals);
            var findAnimal = from animals in humaneSocietyData.Animals
                        where animals.Animal_ID == animalSelectedID
                        select animals;
            foreach (Animal animal in findAnimal)
            {
                animal.HasShoots = menu.SetAnimalShotStatus();
            }
            humaneSocietyData.SubmitChanges();
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
        public void GetPayment()
        {
            Payment adopterPayment = new Payment();
            adopterPayment.PaymentDate = DateTime.Today;
            adopterPayment.Animal_ID = menu.SelectAnimal(humaneSocietyData.Animals);
            adopterPayment.Adopter_ID = menu.SelectAdopter(humaneSocietyData.Adopters);
            adopterPayment.PaymentAmount = menu.GetPayment(humaneSocietyData.Animals);

            humaneSocietyData.Payments.InsertOnSubmit(adopterPayment);
            humaneSocietyData.SubmitChanges();
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
                Console.WriteLine($"{animal.Name} is in Room\t{animal.Room.Number}");
            }
            Console.ReadLine();
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
                    SearchAnimals();
                    break;
                default:
                    Console.WriteLine("Wrong selection, try again. Press ENTER to continue");
                    Console.ReadLine();
                    RunAdopterFunctions();
                    break;
            }
        }
        public void SearchAnimals()
        {

            int categoryToSearch = menu.SearchByCaegory(humaneSocietyData.AnimalTypes);
            decimal maxPrice = menu.PromptForMaxPrice();
            decimal minPrice = menu.PromptForMinPrice();
            string hasShoots = menu.SetAnimalShotStatus();
            var query = from a in humaneSocietyData.Animals
                        where a.AnimalType_ID == categoryToSearch &&
                        a.Price < maxPrice &&
                        a.Price > minPrice &&
                        a.HasShoots == hasShoots
                        select a;
            Console.WriteLine("The following animals meet your search criteria");
            foreach (Animal a in query)
            {
                Console.WriteLine($"{a.Name}\t{a.AnimalType.TypeName}\t{a.Price}");
            }
            Console.ReadLine();
        }
        public void CreateAdopterProfile()
        {
            Adopter addedAdopter = new Adopter();

            addedAdopter.FirstName = menu.PromptForFirstName();
            addedAdopter.LastName = menu.PromptForLastName();
            addedAdopter.DOB = menu.PromptForDOB();
            addedAdopter.AnnualIncome = menu.PromptForAnnualIncome();
            addedAdopter.Bio = menu.PromptForBio();
            addedAdopter.Gender_ID = menu.PromptForGender();

            humaneSocietyData.Adopters.InsertOnSubmit(addedAdopter);
            humaneSocietyData.SubmitChanges();

            Console.ReadLine();
        }
        public void AddAnimalToDatabase()
        {
            Animal addedAnimal = new Animal();

            addedAnimal.Name = menu.PromptForAnimalName();
            addedAnimal.DOB = menu.PromptForDOB();
            addedAnimal.Price = menu.PromptForAnimalPrice();
            addedAnimal.HasShoots = menu.SetAnimalShotStatus();
            addedAnimal.AnimalType_ID = menu.SetAnimalType(humaneSocietyData.AnimalTypes);
            addedAnimal.Food_ID = menu.SetAnimalFoodType(humaneSocietyData.Foods);
            addedAnimal.Room_ID = menu.SetRoomNumber(humaneSocietyData.Rooms);
            addedAnimal.Gender_ID = menu.PromptForGender();
            addedAnimal.AdoptionStatus = menu.SetAnimalAdoptionStatus();

            humaneSocietyData.Animals.InsertOnSubmit(addedAnimal);
            humaneSocietyData.SubmitChanges();
        }
        
        public void OpenHumaneSociety()
        {
            userResponse = menu.EmplyeeOrAdopter();

            if (userResponse == "1")
            {
                RunAdopterFunctions();
            }
            else if (userResponse == "2")
            {
                RunEmployeeFunctions();
            }
            else
            {
                Console.WriteLine("Please select a valid option");
                OpenHumaneSociety();
            }

        }
    }
}
