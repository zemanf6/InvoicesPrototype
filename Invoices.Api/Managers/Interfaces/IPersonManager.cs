using Invoices.Api.Models;

namespace Invoices.Api.Managers.Interfaces
{
    public interface IPersonManager
    {
        IEnumerable<PersonDto> GetAll();
        PersonDto? GetById(int id);
        PersonDto Create(PersonDto dto);
        void Delete(int id);
    }
}
