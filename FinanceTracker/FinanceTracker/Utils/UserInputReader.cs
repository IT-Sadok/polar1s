using FinanceTracker.Enums;
using FinanceTracker.Models;
using System.Globalization;

namespace FinanceTracker.Utils
{
    public class UserInputReader
    {
        private const string FILEPATH = "categories.json";

        public DateTime GetDate()
        {
            DateTime date = DateTime.UtcNow;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Please, enter a date (dd/MM/yyyy): ");
                string? userInput = Console.ReadLine();

                isValid = DateTime.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isValid)
                    Console.WriteLine("Invalid date format. Try again.");
            }

            return date;
        }

        public int GetCategory(OperationType type)
        {
            List<Category> categories = JsonFileManager.ReadCategoriesFromJson(FILEPATH);
            categories = categories.Where(category => category.Type == type).ToList();

            string userInput;
            const string displayMessage = "Enter a category from listed below:";

            while (true)
            {
                userInput = GetCategoryChoice(displayMessage, categories).ToLower();

                var matchingCategory = categories.FirstOrDefault(category => category.Name?.ToLower() == userInput);

                if (matchingCategory != null)
                {
                    return matchingCategory.Id;
                }
            }
        }

        public int GetCategory(Account account)
        {
            List<Category> categories = JsonFileManager.ReadCategoriesFromJson(FILEPATH);
            const string displayMessage = "Enter a category to get records from:";
            string userInput = "";


            while (true)
            {
                userInput = GetInput(displayMessage);

                var matchingCategory = categories.FirstOrDefault(category => category.Name?.ToLower() == userInput);

                if (matchingCategory != null)
                {
                    return matchingCategory.Id;
                }
            }
        }
        public decimal GetAmount()
        {
            bool validEntry = false;
            decimal amount = 0.0m;
            string userInput;
            const string displayMessage = "Enter an amount (i.e 3.45):";

            while (!validEntry)
            {
                userInput = GetInput(displayMessage);
                validEntry = decimal.TryParse(userInput, out amount);
            }

            return Math.Abs(amount);
        }

        public string GetDescription()
        {
            const string displayMessage = "Enter a description:";
            string description = GetInput(displayMessage);

            return description;
        }

        private string GetInput(string displayMessage)
        {
            string? userInput;
            bool validEntry = false;
            string description = "";

            do
            {
                Console.WriteLine(displayMessage);

                userInput = Console.ReadLine();
                if (userInput != null)
                {
                    description = userInput.Trim();
                    validEntry = true;
                }
            } while (validEntry == false);

            return description;
        }

        private string GetCategoryChoice(string displayMessage, List<Category> categories)
        {
            string? userInput;
            bool validEntry = false;
            string description = "";

            Console.WriteLine(displayMessage);
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Name}");
            }

            do
            {
                userInput = Console.ReadLine();
                if (userInput != null)
                {
                    description = userInput.Trim();
                    validEntry = true;
                }
            } while (validEntry == false);

            return description;
        }
    }
}
