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

    public string GetSearchChoice()
    {
        return GetNonEmptyInput("Search by (1 - Author, 2 - Name): ");
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
