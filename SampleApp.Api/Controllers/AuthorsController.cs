using MediatR;

using Microsoft.AspNetCore.Mvc;

using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.MediatR.Queries;

using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllAuthorsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var query = new GetAuthorWithCoursesQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return result != null
                ? (IActionResult)Ok(result)
                : NotFound();
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddAuthorCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction("Get", new { id = result.Id }, result);
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            
            return result == null
                ?  (IActionResult) NotFound()
                : CreatedAtAction("Get", new {id}, result);
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new RemoveAuthorCommand {AuthorId = id};
            _ = await _mediator.Send(command);

            return NoContent();
        }
    }
}