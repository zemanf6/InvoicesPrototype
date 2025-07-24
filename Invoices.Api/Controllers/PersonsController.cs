// using FluentValidation;
// using Microsoft.AspNetCore.Mvc.ModelBinding;
using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
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

        [HttpGet("{id}")]
        public ActionResult<PersonDto> GetById(int id)
        {
            PersonDto? person = _personManager.GetById(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public ActionResult<PersonDto> Create([FromBody] PersonDto dto)
        {
            /*
             * Případně lze nad rámec zadání implementovat validaci pomocí knihovny fluentValidation
             * Ukázka kódu níže
            var result = _validator.Validate(dto);
            if (!result.IsValid)
            {
                ModelStateDictionary modelState = new();

                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return ValidationProblem(modelState);
            }
            */

            PersonDto? createdPerson = _personManager.Create(dto);

            if (createdPerson is null)
                return Conflict("A person with this identification number already exists.");

            return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
        }

        [HttpPut("{id}")]
        public ActionResult<PersonDto> Edit(int id, [FromBody] PersonDto dto)
        {
            PersonDto? updatedPerson = _personManager.Edit(id, dto);
            if (updatedPerson is null)
                return NotFound();

            return Ok(updatedPerson);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _personManager.Delete(id);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("statistics")]
        public ActionResult<IList<PersonStatisticsDto>> GetStatistics()
        {
            var stats = _personManager.GetStatistics();
            return Ok(stats);
        }
    }
}
