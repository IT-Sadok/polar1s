using Booker.Application.Contracts;
using Booker.Domain.Entity;
using Booker.Infrastructure;

namespace Booker.Application.Commands;

public class BorrowBookCommand : ICommand<bool>
{
    public bool Execute(List<Book> books)
    {
        var reader = new UserInputReader();
        var id = reader.GetBookId();

        var book = books.FirstOrDefault(b => b.Id == id && b.Status == Status.Available);
        if (book is null)
        {
            Console.WriteLine("Book not found or already booked.");
            return false;
        }

        book.Status = Status.Booked;
        Console.WriteLine($"Book \"{book.Name}\" is now booked.");
        return true;
    }
}
