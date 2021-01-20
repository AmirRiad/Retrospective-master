using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospective.Models
{
    public class ActionPlanModelView
    {
        public string Comment { get; set; }
        public string sprintName { get; set; }
        public long sprintCommentId { get; set; }
        public string sprintComment { get; set; }
        public List<string> decsions { get; set; }
    }
}