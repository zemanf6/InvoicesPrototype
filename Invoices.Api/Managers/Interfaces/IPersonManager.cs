using Invoices.Api.Models;

namespace Invoices.Api.Managers.Interfaces
{
    public interface IPersonManager
    {
        IEnumerable<PersonDto> GetAll();
        PersonDto? GetById(int id);
        PersonDto? Create(PersonDto dto);
        PersonDto? Edit(int id, PersonDto dto);
        bool Delete(int id);
        IList<PersonStatisticsDto> GetStatistics();
    }
}
