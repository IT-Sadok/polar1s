using Booker.Domain.Entity;

namespace Booker.Application.Contracts
{
    public interface ICommand<TEntity>
    {
        TEntity Execute(List<Book> books);
    }
}
