using Booker.Application.Const;
using Booker.Application.Contracts;
using Booker.Domain.Entity;
using Booker.Infrastructure;

namespace Booker.Application.Commands;

public class SearchBookCommand : ICommand<List<Book>>
{
    public List<Book> Execute(List<Book> books)
    {
        var reader = new UserInputReader();

        var choice = reader.GetSearchChoice();
        var term = reader.GetSearchTerm();

        return choice == SearchOptions.ByAuthor
            ? books.Where(b => b.Author.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList()
            : books.Where(b => b.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
