//using FluentValidation;
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

        public PersonsController(IPersonManager personManager/*, IValidator<PersonDto> validator */)
        {
            _personManager = personManager;
            // _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetAll()
        {
            IEnumerable<PersonDto> people = _personManager.GetAll();
            return Ok(people); // HTTP 200 OK + data v těle odpovědi
        }

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
