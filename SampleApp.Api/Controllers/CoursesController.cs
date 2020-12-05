using Microsoft.AspNetCore.Mvc;

using SampleApp.Core;
using SampleApp.Core.Domain;

using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: api/<CoursesController>
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _unitOfWork.Courses.GetCoursesWithAuthors();
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public Course Get(int id)
        {
            return _unitOfWork.Courses.Get(id);
        }

        // POST api/<CoursesController>
        [HttpPost]
        public void Post([FromBody] Course value)
        {
            _unitOfWork.Courses.Add(value);
            _unitOfWork.SaveChanges();
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Course value)
        {
            var course = _unitOfWork.Courses.Get(id);
            course.Name = value.Name;
            course.Author = value.Author;
            _unitOfWork.SaveChanges();
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var course = _unitOfWork.Courses.Get(id);
            _unitOfWork.Courses.Remove(course);
            _unitOfWork.SaveChanges();
        }
    }
}