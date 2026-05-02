using Booker.Application.Contracts;
using Booker.Domain.Entity;

namespace Booker.Application.Commands;

public class ViewAllBooksCommand : ICommand<List<Book>>
{
    public List<Book> Execute(List<Book> books)
    {
        return books.ToList();
    }
}
