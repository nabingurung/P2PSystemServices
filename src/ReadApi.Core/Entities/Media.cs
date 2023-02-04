using ReadApi.Core.Common;

namespace ReadApi.Core.Entities
{
    public class Media 
    {
       public long MediaId { get; set; }
        public string ImageId { get; set; } = default!;
        public int MediaStatus { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;

        public long VioId { get; set; }
        public virtual  Violation Violation { get; set; }
        //public long ViolationId { get; set; }
        //public Violation? Violation { get; set; }

    }
}
