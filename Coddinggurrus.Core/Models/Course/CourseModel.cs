using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Models.Course
{
    public class CourseModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class CourseWithCount : CourseModel
    {
        public int TotalCount { get; set; }

    }
}
