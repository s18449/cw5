using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wyklad5.DTOs.Requests;

namespace Wyklad5.Services
{
    public interface IStudentDbService
    {
        void EnrollStudent(EnrollStudentRequest request);
        void PromoteStudents(int semester, string studies);
    }
}
