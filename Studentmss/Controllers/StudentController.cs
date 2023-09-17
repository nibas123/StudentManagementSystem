using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Studentms.Models;
using Studentms.Repositories;

namespace Studentms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _StudentRepository;

        public StudentController(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student Student)
        {
            var id = await _StudentRepository.Create(Student);

            return new JsonResult(id.ToString());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var Student = await _StudentRepository.Get(ObjectId.Parse(id));

            return new JsonResult(Student);
        }

        [HttpGet("Fetch")]
        public async Task<IActionResult> Get()
        {
            var Students = await _StudentRepository.Get();

            return new JsonResult(Students);
        }

        [HttpGet("ByName/{Name}")]
        public async Task<IActionResult> GetByName(string Name)
        {
            var Students = await _StudentRepository.GetByName(Name);

            return new JsonResult(Students);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Student Student)
        {
            var result = await _StudentRepository.Update(ObjectId.Parse(id), Student);

            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _StudentRepository.Delete(ObjectId.Parse(id));

            return new JsonResult(result);
        }
    }
}
