﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Degree_Application.Data.CustomData;

namespace Degree_Application.Models
{
    public class ItemModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Please enter 50 characters or less"), MinLength(1)]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Please enter 255 characters or less"), MinLength(1)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
            
        [Required]
        public StatusEnum Status { get; set; }

        //[Required]
        //public SaleStatus SaleStatus { get; set; }

        public ImageModel Image { get; set; }

       // [Required]
        public AccountModel Account { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }
        
    }
}
