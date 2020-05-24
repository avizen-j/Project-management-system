using project_management_system.Enums;
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

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public Enums.Type Type { get; set; }

        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public Project Project { get; set; }

        public int ProjectID { get; set; }

        public IList<AssignmentUser> AssignmentUsers { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}
