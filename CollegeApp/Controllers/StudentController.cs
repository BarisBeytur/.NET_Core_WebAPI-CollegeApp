using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace CollegeApp.Controllers
{


    [Produces("application/xml")] // for xml
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {


        
        [HttpGet]
        [Route("All", Name = "GetAllStudents")] // --> 1. route tanimlama yontemi
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {

            var students = CollegeRepository.Students.Select(i => new StudentDTO()
                {
                    Id = i.Id,
                    StudentName = i.StudentName,
                    Address = i.Address,
                    Email = i.Email,
                }).ToList();

            // OK - 200 - Success
            return Ok(students);
        }







        // -----------------------------------------------------------------------------------------------------------------------------

        // [HttpGet("{id:int}", Name = "GetStudentById")] --> 2. route tanimlama yontemi
        // [ProducesResponseType(hata_kodu)] --> API sayfasinda hatalara karsi dokumantasyon olusturur.
        // ActionResult'a student deger tipini vermeden kullanim --> typeof kullanimi [ProducesResponseType(200, Type = typeof(Student))]
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("{id:int}", Name = "GetStudentById")]
        // Hata kodu yerine StatusCodes.hata_ismi de kullanilabilir.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            // BadRequest - 400 - BadRequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(i => i.Id == id).FirstOrDefault();
            // NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with id {id} not found");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email,
            };

            // OK - 200 - Success
            return Ok(studentDTO);
        }

        // -----------------------------------------------------------------------------------------------------------------------------






        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            // BadRequest - 400 - BadRequest - Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Where(i => i.StudentName == name).FirstOrDefault();
            // NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with name {name} not found");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };

            // OK - 200 - Success
            return Ok(studentDTO);
        }







        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("Create")]
        // api/student/create
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
                return BadRequest();

            //   ---- 1. Directly adding error message to modelstate

            //if(model.AdmissionDate <= DateTime.Now)
            //{

            //    ModelState.AddModelError("AdmissionDate Error","Admission date must be greater than or equal to todays date");
            //    return BadRequest(ModelState);          
            //}

            //    ---- 2. Using custom attribute -> Validators klasorunde tanimlanir.


            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;

            Student student = new Student()
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
            };

            CollegeRepository.Students.Add(student);

            model.Id = student.Id;



            // Status - 201
            // https://localhost:44349/api/Student/4
            // New student details 
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model); // header  kismina yeni kaydin urlsini ekler.
        }






        [ProducesResponseType(StatusCodes.Status204NoContent)] // Herhangi bir icerik donmez.
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Update")]
        [HttpPut]
        // api/student/update
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();

            var existingStudent = CollegeRepository.Students.Where(i => i.Id == model.Id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Address = model.Address;
            existingStudent.Email = model.Email;

            return NoContent(); // Herhangi bir icerik donmez.
        }






        // -----------------------------------------------------------------------------------------------------------------------------

        // http put ile guncelleme yaptigimizda tek satır guncellense bile tum veriyi karsiya gonderir.

        // Cozum olarak http patch uygulanir.

        // Nuget packages -> Microsoft.AspNetCore.JsonPatch
        // Nuget packages -> Microsoft.AspNetCore.Mvc.NewtonsoftJson

        // program.cs -> builder.Services.AddControllers().AddNewtonsoftJson(); olarak guncellenir.

        // indirilmesi gerekir.



        [ProducesResponseType(StatusCodes.Status204NoContent)] // Herhangi bir icerik donmez.
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:int}/UpdatePartial")]
        [HttpPatch]
        // api/student/id/updatepartial
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = CollegeRepository.Students.Where(i => i.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address,
            };


            patchDocument.ApplyTo(studentDTO, ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Address = studentDTO.Address;
            existingStudent.Email = studentDTO.Email;

            return NoContent(); // Herhangi bir icerik donmez.
        }

        // -----------------------------------------------------------------------------------------------------------------------------






        [HttpDelete("Delete/{id}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // api/student/delete/id
        public ActionResult<bool> DeleteStudent(int id)
        {
            // BadRequest - 400 - BadRequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(i => i.Id == id).FirstOrDefault();
            // NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with id {id} not found");

            CollegeRepository.Students.Remove(student);

            // OK - 200 - Success
            return Ok(true);
        }










    }
}
