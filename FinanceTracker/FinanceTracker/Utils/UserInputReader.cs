using FinanceTracker.Enums;
using FinanceTracker.Models;

namespace FinanceTracker.Utils
{
    public class UserInputReader
    {

        /*
        Никак не могу понять, как правильно сделать так, чтобы я возвращал
        нужный класс в зависимости от типа операции
        Начал что-то писать, но застопорился
        Есть ощущение, что это можно как-то красиво сделать, но я пока не знаю как
         */
        public CategoryBase GetCategory(OperationType type)
        {
            string userInput;
            string displayMessage;
            bool validEntry = false;

            while (!validEntry)
            {
                if (type is OperationType.Income)
                {
                    CategoryIncomeEnum categoryIncome;
                    displayMessage = @"Choose an income category:
                                       1 - Salary
                                       2 - Business
                                       3 - Rent
                                       0 - Other";

                    userInput = GetInput(displayMessage);
                    validEntry = Enum.TryParse(userInput, out categoryIncome);

                }
                else
                {
                    CategoryExpenceEnum categoryExpence;
                    displayMessage = @"Choose an expence category:
                                       1 - Food
                                       2 - Rent
                                       3 - Health
                                       4 - Entertainment
                                       0 - Other";

                    userInput = GetInput(displayMessage);
                    validEntry = Enum.TryParse(userInput, out categoryExpence);
                }

            }
        }
        public decimal GetAmount()
        {
            bool validEntry = false;
            decimal amount = 0.0m;
            string userInput;
            string displayMessage = "Enter an amount (i.e 3.45):";

            while (!validEntry)
            {
                userInput = GetInput(displayMessage);
                validEntry = decimal.TryParse(userInput, out amount);
            }

            return Math.Abs(amount);
        }

        public string GetDescription()
        {
            string displayMessage = "Enter a description:";
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

        private 
    }
}
