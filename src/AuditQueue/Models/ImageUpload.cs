using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditQueue.Models
{
    public class ImageUpload
    {
        public string FileName { get; set; }
      
        public string Location { get; set; }
       
        public string ViolationDate { get; set; }
      
        public string ViolationTime { get; set; }
       
        public string Base64StringData { get; set; }

        public string Description { get; set; }
    }
}
