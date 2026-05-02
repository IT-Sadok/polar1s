using Booker.Application.Contracts;
using Booker.Domain.Entity;
using Booker.Infrastructure;

namespace Booker.Application.Commands;

public class DeleteBookCommand : ICommand<bool>
{
    public bool Execute(List<Book> books)
    {
        var reader = new UserInputReader();
        var id = reader.GetBookId();

        var book = books.FirstOrDefault(b => b.Id == id);
        if (book is null)
        {
            Console.WriteLine("Book not found.");
            return false;
        }

        books.Remove(book);
        Console.WriteLine($"Book \"{book.Name}\" deleted.");
        return true;
    }
}
