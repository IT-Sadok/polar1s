using Booker.Application.Const;

namespace Booker.Infrastructure;

public class UserInputReader
{
    public string GetBookName()
    {
        return GetNonEmptyInput("Enter book name: ");
    }

    public string GetAuthor()
    {
        return GetNonEmptyInput("Enter author: ");
    }

    public int GetPublishYear()
    {
        while (true)
        {
            Console.Write("Enter publish year: ");
            if (int.TryParse(Console.ReadLine(), out var year))
                return year;

            Console.WriteLine("Invalid year. Try again.");
        }
    }

    public Guid GetBookId()
    {
        while (true)
        {
            Console.Write("Enter book Id: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
                return id;

            Console.WriteLine("Invalid Id format. Try again.");
        }
    }

    public SearchOptions GetSearchChoice()
    {
        while (true)
        {
            var choice = GetNonEmptyInput("Search by (1 - Author, 2 - Name): ");
            if (choice == "1")
            {
                return SearchOptions.ByAuthor;
            }
            else if (choice == "2")
            {
                return SearchOptions.ByName;
            }

            Console.WriteLine("Invalid choice. Enter 1 or 2.");
        }
    }

    public string GetSearchTerm()
    {
        return GetNonEmptyInput("Enter search term: ");
    }

    private string GetNonEmptyInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input))
                return input;

            Console.WriteLine("Input cannot be empty. Try again.");
        }
    }
}
