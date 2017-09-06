using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HumaneSocietyConsole
{
    class HumaneSociety
    {
        private string userResponse;
        private HumaneSocietyDataContext humaneSocietyData = new HumaneSocietyDataContext();
        Func<IQueryable,int> getPrimaryKey;
        delegate int GetPrimaryKeyFunction(Table<Adopter> adopter);
        private Adopter addedAdopter;

        Menu menu;
        public HumaneSociety(Menu menu)
        {
            this.menu = menu;
            this.addedAdopter = new Adopter();
        }
        public void ReturnToMainMenu()
        {
            Console.WriteLine("\nPress ENTER to continue");
            Console.Read();
            OpenHumaneSociety();
        }
        private void RunDeveloperAdminFunctions()
        {
            userResponse = menu.DisplayDeveloperAdminMenu();
            switch (userResponse)
            {
                case "1":
                    ImportFile();
                    break;
                default:
                    Console.WriteLine("Please enter a valid response. Press ENTER to continue");
                    Console.ReadLine();
                    RunDeveloperAdminFunctions();
                    break;
            }

        }
        private void ImportFile()
        {
            
            List<string> somelist = new List<string>();
            var csvLines = File.ReadAllLines("C:/Users/warne/Documents/Devcodecamp/HumaneSociety/AnimalImportFileTest.csv").Select(x => x.Split(','));

            foreach (string[] line in csvLines)
            {
                Animal animalToAdd = new Animal();
                animalToAdd.Name = line[0];
                animalToAdd.DOB = Convert.ToDateTime(line[1]);
                animalToAdd.Price = Convert.ToDecimal(line[2]);
                animalToAdd.HasShoots = line[3];
                animalToAdd.Adopter_ID = null;
                animalToAdd.AnimalType_ID = Convert.ToInt32(line[5]);
                animalToAdd.Food_ID = Convert.ToInt32(line[6]);
                animalToAdd.Room_ID = Convert.ToInt32(line[7]);
                animalToAdd.Gender_ID = Convert.ToInt32(line[8]);
                animalToAdd.AdoptionStatus = line[9];
                humaneSocietyData.Animals.InsertOnSubmit(animalToAdd);
                humaneSocietyData.SubmitChanges();
            }
            Console.WriteLine("Press ENTER to continue");            
            Console.ReadLine();
            OpenHumaneSociety();
        }
        private void RunEmployeeFunctions()
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
        private void UpdateAnimalShotStatus()
        {
            Animal animalSelected = new Animal();
            int animalSelectedID = menu.ShowShotStatus(humaneSocietyData.Animals);
            var findAnimal = from animals in humaneSocietyData.Animals
                        where animals.Animal_ID == animalSelectedID
                        select animals;
            if (findAnimal.Count() > 0)
            {
                foreach (Animal animal in findAnimal)
                {
                    animal.HasShoots = menu.SetAnimalShotStatus();
                }
                humaneSocietyData.SubmitChanges();
                ReturnToMainMenu();
            }
            else
            {
                Console.WriteLine("\nYou have keyed in an invalid ID, please try again.");
                UpdateAnimalShotStatus();
            }
            
        }
        private void SetAdoptionStatus()
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
            Console.WriteLine($"{animalToUpdate.Name} adoption status is now - {animalToUpdate.AdoptionStatus}.");
            ReturnToMainMenu();
        }
        private void GetPayment()
        {
            //func testing
            getPrimaryKey = menu.SelectAnimal;
            //delegate testing
            GetPrimaryKeyFunction getPrimaryKeyDelegate;
            getPrimaryKeyDelegate = menu.SelectAdopter;

            Payment adopterPayment = new Payment();

            adopterPayment.PaymentDate = DateTime.Today;
            adopterPayment.Animal_ID = getPrimaryKey(humaneSocietyData.Animals.OrderBy(x => x.AnimalType_ID));
            adopterPayment.Adopter_ID = getPrimaryKeyDelegate(humaneSocietyData.Adopters);
            adopterPayment.PaymentAmount = menu.GetPayment(humaneSocietyData.Animals);

            try
            {
                humaneSocietyData.Payments.InsertOnSubmit(adopterPayment);
                humaneSocietyData.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong\n{e.Message}\nPress Enter to continue.");
                Console.ReadLine();
                Console.Clear();
                GetPayment();
            }            

            ReturnToMainMenu();
        }
        private void CalculateFoodNeeds()
        {
            userResponse = menu.GetUserFoodNeed();
            var animals = from a in humaneSocietyData.Animals
                          select a;
            foreach (Animal animal in animals.OrderBy(a => a.AnimalType_ID))
            {
                Console.WriteLine($"Name: {animal.Name}\nServings needed for {userResponse} weeks: {animal.Food.WeeklyServing * Convert.ToInt32(userResponse)}\nFood: {animal.Food.Name}\n");
            }
            ReturnToMainMenu();
        }
        private void ListAnimalCategories()
        {
            var animals = from a in humaneSocietyData.Animals
                          select a;
            foreach (Animal animal in animals.OrderBy(a => a.AnimalType_ID))
            {
                Console.WriteLine($"{animal.AnimalType.TypeName} - {animal.Name}");
            }
            ReturnToMainMenu();
        }
        private void ListAnimalRoomNumnbers()
        {
            var animals = from a in humaneSocietyData.Animals
                          select a;
            foreach (Animal animal in animals.OrderBy(a => a.AnimalType_ID))
            {
                Console.WriteLine($"Room# {animal.Room.Number} is occupied by {animal.AnimalType.TypeName} - {animal.Name}");
            }
            ReturnToMainMenu();
        }
        private void RunAdopterFunctions()
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
        private void SearchAnimals()
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
            if (query.Count() > 0)
            {
                Console.WriteLine("The following animals meet your search criteria");
                foreach (Animal a in query)
                {
                    Console.WriteLine($"{a.Name}\t{a.AnimalType.TypeName}\t{a.Price}");
                }
                ReturnToMainMenu();
            }
            else
            {
                Console.WriteLine("There are no animals that meet your criteria,  please try again.");
                ReturnToMainMenu();
            }
        }
        private void CreateAdopterProfile()
        {
            addedAdopter.FirstName = menu.PromptForFirstName();
            addedAdopter.LastName = menu.PromptForLastName();
            addedAdopter.DOB = menu.PromptForDOB();
            addedAdopter.AnnualIncome = menu.PromptForAnnualIncome();
            addedAdopter.Bio = menu.PromptForBio();
            addedAdopter.Gender_ID = menu.PromptForGender();
            try
            {
                humaneSocietyData.Adopters.InsertOnSubmit(addedAdopter);
                humaneSocietyData.SubmitChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something went wrong.\n{exception}\nPlease try again.");
                CreateAdopterProfile();
            }
            Console.WriteLine("Your profile has been created.");

            ReturnToMainMenu();
        }
        private void AddAnimalToDatabase()
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

            ReturnToMainMenu();
        }
        
        public void OpenHumaneSociety()
        {
            userResponse = menu.EmployeeAdopterOrDeveloper();

            if (userResponse == "1")
            {
                RunAdopterFunctions();
            }
            else if (userResponse == "2")
            {
                RunEmployeeFunctions();
            }
            else if (userResponse == "3")
            {
                RunDeveloperAdminFunctions();
            }
            else
            {
                Console.WriteLine("Please select a valid option");
                OpenHumaneSociety();
            }
        }
        
    }
}
