using Microsoft.AspNetCore.Mvc;

using SampleApp.Core;
using SampleApp.Core.Domain;

using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return _unitOfWork.Authors.GetAll();
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public Author Get(int id)
        {
            return _unitOfWork.Authors.GetAuthorWithCourses(id);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public void Post([FromBody] Author value)
        {
            _unitOfWork.Authors.Add(value);
            _unitOfWork.SaveChanges();
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Author value)
        {
            var author = _unitOfWork.Authors.Get(id);
            author.Name = value.Name;
            author.Courses = value.Courses;
            _unitOfWork.SaveChanges();
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var author = _unitOfWork.Authors.Get(id);
            _unitOfWork.Authors.Remove(author);
            _unitOfWork.SaveChanges();
        }
    }
}