//using FluentValidation;
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

        public PersonsController(IPersonManager personManager/*, IValidator<PersonDto> validator */)
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

        [HttpPost]
        public ActionResult<PersonDto> Create([FromBody] PersonDto dto)
        {
            /*
             * Případně lze nad rámec zadání implementovat validaci pomocí knihovny fluentValidation
             * Ukázka kódu níže
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

            // TODO: až bude implementován detail (GET /api/persons/{id}), lze vracet tohle:
            // return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

            return Created(string.Empty, createdPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _personManager.Delete(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
