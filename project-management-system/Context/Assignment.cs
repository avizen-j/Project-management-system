using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Context
{
    public class Assignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssignmentID { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

        public IList<AssignmentUser> AssignmentUsers { get; set; }
    }
}
