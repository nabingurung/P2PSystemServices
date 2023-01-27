﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditQueue.DbModels
{
    [Table("media")]
    public class Media
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("image_uid",TypeName ="varchar(50)")]
        public string ImageUID { get; set; } = default!;
        [Column("status")]
        public int Status { get; set; } = 0;
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Column("trans_date")]
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
      
    
    }
}
