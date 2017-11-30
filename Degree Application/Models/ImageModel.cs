using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Degree_Application.Models
{
    public class ImageModel
    {
        public int Id { get; set; }

        [DataType(DataType.Upload)] 
        public string Image { get; set; }
    }
}
