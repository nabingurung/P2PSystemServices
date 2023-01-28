using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditQueue.DbModels
{
    [Table("media")]
    public partial class Media
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
