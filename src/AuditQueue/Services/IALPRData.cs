using AuditQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditQueue.Services
{
    public interface IALPRData
    {
        Task GetAlprImageAsync(AlprDataDto alprDataDto);
    }
}
