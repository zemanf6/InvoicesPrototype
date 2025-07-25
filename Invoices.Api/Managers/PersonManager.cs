/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *
 *                      ___ ___ ___
 *                     | . |  _| . |  LICENCE
 *                     |  _|_| |___|
 *                     |_|
 *
 *    REKVALIFIKAČNÍ KURZY  <>  PROGRAMOVÁNÍ  <>  IT KARIÉRA
 *
 * Tento zdrojový kód je součástí profesionálních IT kurzů na
 * WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci PRO obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */

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
