using System;
using System.ComponentModel.DataAnnotations;

namespace jober.Models
{
    public class Users
    {
        public string id { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        [Required,MinLength(4,ErrorMessage ="password should be more than 4 character"),DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public Users()
        {

        }
    }
}

