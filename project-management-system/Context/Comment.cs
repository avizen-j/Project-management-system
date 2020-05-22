using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Context
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommentID { get; set; }

        public string CommentContent { get; set; }

        public DateTime CreationTime { get; set; }

        public Assignment Assignment { get; set; }

        public int AssignmentID { get; set; }
    }
}
