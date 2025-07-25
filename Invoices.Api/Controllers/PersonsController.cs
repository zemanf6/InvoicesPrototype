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

// using FluentValidation;
// using Microsoft.AspNetCore.Mvc.ModelBinding;
using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    /// <summary>
    /// API kontroler pro práci s osobami (Person).
    /// Slouží jako vstupní bod pro HTTP požadavky.
    /// Veškerá logika je delegována do manageru – controller pouze zpracovává vstup a výstup.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonManager _personManager;
        // private readonly IValidator<PersonDto> _validator;

        public PersonsController(IPersonManager personManager /*, IValidator<PersonDto> validator*/)
        {
            _personManager = personManager;
            // _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetAll()
        {
            IEnumerable<PersonDto> people = _personManager.GetAll();
            return Ok(people);
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <remarks>
        /// Zde třeba příklad toho, co lze poslat v requestu
        /// Bonus rozšíření: Přidat swagger dokumentaci k endpointům
        /// </remarks>
        /// <param name="dto">The person data to create</param>
        /// <returns>The created person</returns>
        /// <response code="201">Returns the newly created person</response>
        /// <response code="400">If the input is invalid</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<PersonDto> Create([FromBody] PersonDto dto)
        {
            /*
             * Bonus rozšíření: Validace pomocí fluent validation knihovny
             * viz soubor Validators/PersonDtoValidator.cs, ukázka kódu níže,
             * nutno aktivovat zakomentovaný kód v souboru
            var result = _validator.Validate(dto);
            if (!result.IsValid)
            {
                var modelState = new ModelStateDictionary();

                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            
                return ValidationProblem(modelState);
            }
            */
            PersonDto createdPerson = _personManager.Create(dto);

            // V budoucnu: až bude implementován detail (GET /api/persons/{id}),
            // lze vracet odkaz na nově vytvořený záznam v location hlavičce:
            // return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

            return Created(string.Empty, createdPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _personManager.Delete(id);

            if (!success)
                return NotFound(); // HTTP 404 – pokud osoba neexistuje

            return NoContent(); // HTTP 204 – smazáno, server nic dále nevrací
        }
    }
}
