using Admin.Shared.Dtos;
using Admin.Shared.Dtos.mcq;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.McqPastpaper
{
    public interface IMcqPastpersRepository
    {
        
        Task<IEnumerable<MCQDto>> Get(string pastpaperId, string userId);
        Task<IEnumerable<MCQDto>> GetMcqsByTopics(int topicId, string userId);
        Task<IEnumerable<TopicsDto>> GetTopicDtosForGivenSubjectID(int id, string userid);
    }

    
}
