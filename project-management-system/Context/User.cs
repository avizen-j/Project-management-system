using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Context
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Project Project { get; set; }
        public IList<AssignmentUser> AssignmentUsers { get; set; }
    }
}
