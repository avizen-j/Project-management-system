using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Context
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
