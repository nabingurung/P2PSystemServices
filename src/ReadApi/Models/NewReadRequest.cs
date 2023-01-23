namespace ReadApi.Models
{
    // DTO object for Read. Used on ReadController to post the new Data.
    public class NewReadRequest
    {
        public int Id { get; set; }
        public string? SystemId { get; set; }
    }
}