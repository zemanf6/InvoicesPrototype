using Invoices.Api.Models;

namespace Invoices.Api.Managers.Interfaces
{
    public interface IPersonManager
    {
        IEnumerable<PersonDto> GetAll();
        PersonDto Create(PersonDto dto);
        bool Delete(int id);
    }
}
