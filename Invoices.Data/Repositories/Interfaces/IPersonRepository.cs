using Invoices.Data.Entities;

namespace Invoices.Data.Repositories.Interfaces
{
    public interface IPersonRepository: IRepository<Person>
    {
        IEnumerable<Person> GetByHidden(bool hidden);
    }
}
