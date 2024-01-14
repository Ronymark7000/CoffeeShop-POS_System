using System;
using System.Data;
using System.Text.Json;
using Bislerium_v7.Data;
using Bislerium_v7.Pages;

namespace Bislerium_v7.Data
{
	public class CoffeeService
	{
		public static void SaveAllCoffee(List<Coffees> coffees)
		{
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appCoffeesFilePath = Utils.GetAppCoffeeFilePath();
            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            var json = JsonSerializer.Serialize(coffees);
            File.WriteAllText(appCoffeesFilePath, json);
        }

		public static List<Coffees> AddCoffee(string CoffeeName, int CoffeePrice)
		{
            List<Coffees> coffees = GetAllCoffee();
            coffees.Add(
            new Coffees
            {
                ID = coffees.Count() + 1,
                CoffeeName = CoffeeName,
                CoffeePrice = CoffeePrice,
            }
        );
            SaveAllCoffee(coffees);
            return coffees;
        }

		// Get all coffee if the file exists else return empty list
		public static List <Coffees> GetAllCoffee()
		{
            string coffeeFilePath = Utils.GetAppCoffeeFilePath();
            if (!File.Exists(coffeeFilePath))
            {
                return new List<Coffees>();
            }

            var json = File.ReadAllText(coffeeFilePath);

            return JsonSerializer.Deserialize<List<Coffees>>(json);
        }

		public static List<Coffees> UpdateCoffee(int ID, string newCoffeeName, int newCoffePrice)
		{
            List<Coffees> coffees = GetAllCoffee();
            Coffees coffeeToUpdate = coffees.Find(x => x.ID == ID);
            coffees.Remove(coffeeToUpdate);
            coffees.Add(
            new Coffees
            {
                ID = ID,
                CoffeeName = newCoffeeName,
                CoffeePrice = newCoffePrice,
            });
            SaveAllCoffee(coffees);
            return coffees;
        }

		public static List<Coffees> DeleteCoffee(int ID)
		{
            List<Coffees> coffees = GetAllCoffee();
            Coffees coffeeToDelete = coffees.Find(x => x.ID == ID);

            if (coffeeToDelete == null)
                throw new Exception("Coffee is not found");

            coffees.Remove(coffeeToDelete);
            SaveAllCoffee(coffees);
            return coffees;
        }
	}
}

