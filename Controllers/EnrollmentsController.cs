using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyklad5.DTOs.Requests;
using Wyklad5.DTOs.Responses;
using Wyklad5.Models;
using Wyklad5.Services;

namespace Wyklad5.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
                _service.EnrollStudent(request);
                var response = new EnrollStudentResponse();
                response.LastName = request.LastName;

                return Created("http://localhost:5001/api/students?indexNumber=" + request.IndexNumber, response);
        }
        

        [Route("promotions")]
        [HttpPost]
        public IActionResult PromoteStudents(string studies, int semester)
        {
                _service.PromoteStudents(semester, studies);

                return Ok();
        }
    }

}

