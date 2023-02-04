namespace AuditQueue.Services
{
    public class DownloadImageException : Exception
    {
        public DownloadImageException() { }

        public DownloadImageException(string msg) : base($"Unable to download Image. {msg}")
        {

        }

    }
}
