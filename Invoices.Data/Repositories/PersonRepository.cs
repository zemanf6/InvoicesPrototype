using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Data.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Person> GetByHidden(bool hidden)
        {
            return _dbSet.Where(x => x.Hidden == hidden).ToList();
        }

        public override Person? GetById(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id && !p.Hidden);
        }

        public IList<Person> GetAllByIdentificationNumber(string identificationNumber)
        {
            return _dbSet
                .Where(p => p.IdentificationNumber == identificationNumber)
                .ToList();
        }
    }
}
