using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Context
{
    public class AssignmentUser
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int AssignmentID { get; set; }
        public Assignment Assignment { get; set; }
    }
}
