﻿using System;
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

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18449;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try { 
                com.CommandText = "select IdStudies from studies where name = @name";

                com.Parameters.AddWithValue("name", request.Studies);

                var dr = com.ExecuteReader();

                if (!dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("Studia nie istniaja");

                }

                int idstudies = (int)dr["IdStudies"];

                com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";

                com.Parameters.AddWithValue("index", request.IndexNumber);

                com.ExecuteNonQuery();

                tran.Commit();

            } catch(SqlException exc)
                {
                    tran.Rollback();
                }

            _service.EnrollStudent(request);
            var response = new EnrollStudentResponse();
            response.LastName = st.LastName;
            //...

            return Ok(response);
        }

        //..

        //..


    }
}