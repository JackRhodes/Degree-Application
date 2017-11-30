using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Degree_Application.Models
{
    public class OrderModel
    {

        [Key]
        public int Id { get; set; }
        public ItemModel Item { get; set; }
        public AccountModel Account { get; set; }
        
    }
}
