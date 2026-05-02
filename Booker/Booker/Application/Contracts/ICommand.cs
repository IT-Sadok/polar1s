using Booker.Domain.Entity;

namespace Booker.Application.Contracts
{
    public interface ICommand<TResult>
    {
        TResult Execute(List<Book> books);
    }
}
