using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Degree_Application.Models
{
    public class ImageModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required] 
        public byte[] Image { get; set; }
    }
}
