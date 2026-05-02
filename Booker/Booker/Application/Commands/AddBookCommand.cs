using Booker.Application.Contracts;
using Booker.Domain.Entity;
using Booker.Infrastructure;

namespace Booker.Application.Commands;

public class AddBookCommand : ICommand<bool>
{
    public bool Execute(List<Book> books)
    {
        var reader = new UserInputReader();

        var name = reader.GetBookName();
        var author = reader.GetAuthor();
        var year = reader.GetPublishYear();

        if (books.Any(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                        && b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("A book with the same name and author already exists.");
            return false;
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Name = name,
            Author = author,
            PublishYear = year,
            Status = Status.Available
        };

        books.Add(book);
        Console.WriteLine($"Book added. Id: {book.Id}");
        return true;
    }
}
