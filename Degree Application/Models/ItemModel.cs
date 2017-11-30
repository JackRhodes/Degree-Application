using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Degree_Application.Models
{
    public class ItemModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Please enter 50 characters or less"), MinLength(3)]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Please enter 255 characters or less"), MinLength(1)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
            
        [Required]
        public int Status { get; set; }

        public ImageModel Image { get; set; }

    }
}
