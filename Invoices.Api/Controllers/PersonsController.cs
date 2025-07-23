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

        public PersonsController(IPersonManager personManager)
        {
            _personManager = personManager;
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
            PersonDto createdPerson = _personManager.Create(dto);

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
    }
}
