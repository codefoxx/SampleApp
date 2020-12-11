using MediatR;

using Microsoft.AspNetCore.Mvc;

using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.MediatR.Queries;

using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetCoursesWithAuthorsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetCourseByIdQuery {CourseId = id};
            var result = await _mediator.Send(query);

            return result != null
                ? Ok(result)
                : (IActionResult) NotFound();
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction("Get", new {id = result.Id}, result);
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCourseCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction("Get", new { id = result.Id }, result);
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new RemoveCourseCommand();
            await _mediator.Send(command);

            return NoContent();
        }
    }
}