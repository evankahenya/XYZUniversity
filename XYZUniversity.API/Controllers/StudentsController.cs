using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>A list of students.</returns>
        /// <response code="200">Returns the list of students</response>
        /// <response code="404">If no students are found</response>
        [HttpGet]
        [Authorize (Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database - Domain Models
            var studentsDomain = await studentRepository.GetAllAsync();
            return Ok(mapper.Map<List<StudentDto>>(studentsDomain));
        }

        // GET STUDENT BY ID
        /// <summary>
        /// Gets a single student by ID which is a GUID.
        /// </summary>
        /// <returns>A single student.</returns>
        /// <response code="200">Returns the student</response>
        /// <response code="404">If no students are found</response>
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize (Roles = "Reader,Writer")]
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
        /// <summary>
        ///  Post Method to create new student. Only accessible with the Writer role
        /// </summary>
        /// <returns>The newly enrolled student</returns>
        /// <response code="200">Student added successfully</response>
        /// <response code="401">Not authourized to add new students </response>
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestDto addStudentRequestDto)
        {
            if (ModelState.IsValid)
            {
                // MAP DTO TO DOMAIN MODEL
                var studentDomainModel = mapper.Map<Student>(addStudentRequestDto);

                // USE DOMAIN MODEL TO CREATE STUDENT
                studentDomainModel = await studentRepository.CreateAsync(studentDomainModel);

                // Map domain model back to DTO
                var studentDto = mapper.Map<StudentDto>(studentDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = studentDto.Id }, studentDto);
            }
            else
            {
                return BadRequest();
            }
        }

        // UPDATE STUDENT
        // PUT: https://localhost:portnumber/api/students/{id}
        /// <summary>
        ///  PUT Method to edit existing student. Only accessible with the Writer role
        /// </summary>
        /// <returns>The newly updated student</returns>
        /// <response code="200">Student updated successfully</response>
        /// <response code="401">Not authourized to add new students </response>
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            if (ModelState.IsValid)
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
            else { return BadRequest(); }
        }

        // DELETE STUDENT
        // DELETE: https://localhost:portnumber/api/students/{id}
        /// <summary>
        ///  Post Method to Delete student. Only accessible with the Writer role
        /// </summary>
     
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
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
