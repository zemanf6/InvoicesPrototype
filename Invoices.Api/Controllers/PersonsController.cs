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
            PersonDto created = _personManager.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personManager.Delete(id);
            return NoContent();
        }
    }
}
