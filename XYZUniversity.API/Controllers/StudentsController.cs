using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XYZUniversity.API.Data;
using XYZUniversity.API.Models.Domain;
using XYZUniversity.API.Models.DTO;
using XYZUniversity.API.Repositories;


namespace XYZUniversity.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly XYZUniversityDbContext dbContext; // Replace with your actual DbContext
        private readonly IStudentRepository studentRepository; // Replace with your actual student repository
        private readonly IMapper mapper;

        public StudentsController(XYZUniversityDbContext dbContext, IStudentRepository studentRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        // GET ALL STUDENTS
        // GET: https://localhost:portnumber/api/students
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database - Domain Models
            var studentsDomain = await studentRepository.GetAllAsync();
            return Ok(mapper.Map<List<StudentDto>>(studentsDomain));
        }

        // GET STUDENT BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var studentDomain = await studentRepository.GetByIdAsync(id);
            if (studentDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StudentDto>(studentDomain));
        }

        // POST to create new student
        // POST: https://localhost:portnumber/api/students
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestDto addStudentRequestDto)
        {

            // MAP DTO TO DOMAIN MODEL
            var studentDomainModel = mapper.Map<Student>(addStudentRequestDto);

            // USE DOMAIN MODEL TO CREATE STUDENT
            studentDomainModel = await studentRepository.CreateAsync(studentDomainModel);

            // Map domain model back to DTO
            var studentDto = mapper.Map<StudentDto>(studentDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = studentDto.Id }, studentDto);
        }

        // UPDATE STUDENT
        // PUT: https://localhost:portnumber/api/students/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            // Map DTO to Domain Model
            var studentDomainModel = mapper.Map<Student>(updateStudentRequestDto);

            // Check if student exists
            studentDomainModel = await studentRepository.UpdateAsync(id, studentDomainModel);
            if (studentDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StudentDto>(studentDomainModel));
        }

        // DELETE STUDENT
        // DELETE: https://localhost:portnumber/api/students/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var studentDomainModel = await studentRepository.DeleteAsync(id);
            if (studentDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StudentDto>(studentDomainModel));
        }
    }
}
