using AutoMapper;
using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Api.Managers
{
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
            IEnumerable<Person> people = _personRepository.GetByHidden(false);
            return _mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public PersonDto? GetById(int id)
        {
            Person? person = _personRepository.GetById(id);
            if (person is null)
                return null;

            return _mapper.Map<PersonDto>(person);
        }

        public PersonDto Create(PersonDto dto)
        {
            Person person = _mapper.Map<Person>(dto);
            Person addedPerson = _personRepository.Add(person);
            _personRepository.SaveChanges();
            return _mapper.Map<PersonDto>(addedPerson);
        }

        public PersonDto? Edit(int id, PersonDto dto)
        {
            Person? hidden = HidePerson(id);
            if (hidden is null)
                return null;

            Person? newPerson = _mapper.Map<Person>(dto);
            newPerson.Id = default;
            Person added = _personRepository.Add(newPerson);

            _personRepository.SaveChanges();
            return _mapper.Map<PersonDto>(added);
        }

        public bool Delete(int id)
        {
            if (HidePerson(id) is not null)
            {
                _personRepository.SaveChanges();
                return true;
            }

            return false;
        }

        private Person? HidePerson(int personId)
        {
            Person? person = _personRepository.GetById(personId);

            if (person is null)
                return null;

            person.Hidden = true;
            return _personRepository.Update(person);
        }

        public IList<PersonStatisticsDto> GetStatistics()
        {
            IEnumerable<Person> persons = _personRepository.GetAll();

            List<PersonStatisticsDto> result = persons.Select(p => new PersonStatisticsDto
            {
                PersonId = p.Id,
                PersonName = p.Name,
                Revenue = p.Sales.Sum(i => i.Price)
            }).ToList();

            return result;
        }
    }
}
