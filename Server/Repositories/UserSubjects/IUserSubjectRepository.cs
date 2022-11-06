using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.UserSubjects
{
    public interface IUserSubjectRepository
    {
        Task<IEnumerable<UserSubject>> Get();
        Task<UserSubject> Get(int id);
        Task<UserSubject> Create(UserSubject userSubject, string userId);
        Task<ActionResult<UserSubject>> Update(int id, UserSubject userSubject, string userId);
        Task Delete(int id);
    }

    
}
