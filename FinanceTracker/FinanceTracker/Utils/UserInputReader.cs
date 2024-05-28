using FinanceTracker.Enums;
using FinanceTracker.Models;

namespace FinanceTracker.Utils
{
    public class UserInputReader
    {
        public int GetCategory(OperationType type)
        {
            const string FILEPATH = "categories.json";
            List<Category> categories = JsonFileManager.ReadCategoriesFromJson(FILEPATH);
            categories = categories.Where(category => category.Type == type).ToList();

            bool validEntry = false;
            int categoryId = 0;
            string userInput;
            const string displayMessage = "Enter a category from listed below:";

            while (!validEntry)
            {
                userInput = GetInput(displayMessage, categories).ToLower();
                foreach (var category in categories)
                {
                    if (userInput == category.Name?.ToLower())
                    {
                        categoryId = category.Id;
                        validEntry = true;
                    }
                }
            }

            return categoryId;
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

        private string GetInput(string displayMessage, List<Category> categories)
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
