using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Context
{
    public class UserTask
    {
        [Key]
        public Guid Id { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
