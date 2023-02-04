using AuditQueue.Models;

namespace AuditQueue.Services
{
    public interface IHttpService
    {
        Task<byte[]> DownloadAlprImageAsync(AlprDataDto alprDataDto);

    }
}
