using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Degree_Application.Models
{
    public class AccountModel
    {

        public int AccountId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Must be 8 or more characters"), MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public long Mobile { get; set; }

        [Required]
        [RegularExpression(@"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z]))))\s?[0-9][A-Za-z]{2})",ErrorMessage = "Invalid UK Postcode")]
        public string PostCode { get; set; }

        public string ProfilePicture { get; set; }


    }
}
