using FinanceTracker.Enums;

namespace FinanceTracker
{
    public class UserInputReader
    {
        public decimal GetAmount()
        {
            string? userInput;
            bool validEntry = false;
            decimal amount = 0.0m;

            do
            {
                Console.WriteLine("Enter an amount (i.e 3.45):");

                userInput = Console.ReadLine();
                if (userInput != null)
                {
                    validEntry = decimal.TryParse(userInput, out amount);
                }
            } while (validEntry == false);

            return amount;
        }

        public string GetDescription()
        {
            string? userInput;
            bool validEntry = false;
            string description = "";

            do
            {
                Console.WriteLine("Enter a description:");

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
