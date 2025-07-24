using AutoMapper;
using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Manager vrstva obsahuje aplikační logiku spojenou s entitou Person.
    /// Odděluje controller od přístupu k datům a umožňuje snadnější testování a údržbu.
    /// </summary>
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonManager(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public IEnumerable<PersonDto> GetAll()
        {
            // Vrátíme pouze aktivní (neskryté) osoby
            var people = _personRepository.GetByHidden(false);
            return _mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public PersonDto Create(PersonDto dto)
        {
            // BONUS rozšíření: Lze přidat kontrolu, zda už osoba s tímto IČ neexistuje
            //                  a neshodí tak aplikaci
            Person person = _mapper.Map<Person>(dto);
            Person addedPerson = _personRepository.Add(person);
            _personRepository.SaveChanges();
            return _mapper.Map<PersonDto>(addedPerson);
        }

        public bool Delete(int id)
        {
            // Soft delete: nastavíme příznak Hidden = true místo fyzického smazání
            if (HidePerson(id) != null)
            {
                _personRepository.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Označí osobu jako skrytou (soft delete).
        /// Pokud osoba neexistuje, vrací null.
        /// </summary>
        private Person? HidePerson(int personId)
        {
            Person? person = _personRepository.GetById(personId);

            if (person is null)
                return null;

            person.Hidden = true;
            return _personRepository.Update(person);
        }
    }
}
