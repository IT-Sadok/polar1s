using Booker.Application.Contracts;
using Booker.Domain.Entity;
using Booker.Infrastructure;

namespace Booker.Application.Commands;

public class ReturnBookCommand : ICommand<bool>
{
    public bool Execute(List<Book> books)
    {
        var reader = new UserInputReader();
        var id = reader.GetBookId();

        var book = books.FirstOrDefault(b => b.Id == id && b.Status == Status.Borrowed);
        if (book is null)
        {
            Console.WriteLine("Book not found or not currently borrowed.");
            return false;
        }

        book.Status = Status.Available;
        Console.WriteLine($"Book \"{book.Name}\" is now available.");
        return true;
    }
}
