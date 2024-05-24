using FinanceTracker.Enums;

namespace FinanceTracker.Utils
{
    public class UserInputReader
    {
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


    }
}
