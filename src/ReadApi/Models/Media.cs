using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReadApi.Models
{
    [Table("media")]
    public class Media
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("image_uid", TypeName = "varchar(50)")]
        public string ImageUID { get; set; } = default!;
        [Column("status")]
        public int Status { get; set; } = 0;
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Column("trans_date")]
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;

        public long ViolationId { get; set; }
        public virtual Violation Violation { get; set; }

    }
}
