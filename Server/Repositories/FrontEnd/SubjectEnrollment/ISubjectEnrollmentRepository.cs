using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.SubjectEnrollment
{
    public interface ISubjectEnrollmentRepository
    {
        Task<IEnumerable<UserSubjectsDto>> GetAll(string userId); //Get all enrollments for a given user
        Task<EnrollmentSubjectDto> GetMeEnroll(int subjectId, AppUser user); //Get enrollment for a given subject and subjectId
        //Task<bool> Create(int subjectId, string userid); //Done during user enrollment
        //Task<ActionResult<EnrollmentSubjectDto>> Update(UserSubject usersubject); //Done during a purchase
        Task<ActionResult<IEnumerable<UserSubjectsDto>>> Delete(int id, string userId); //Uneroll a user from a subject
    }

    
}
