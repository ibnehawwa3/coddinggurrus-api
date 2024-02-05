
namespace Coddinggurrus.Core.Entities
{
    public class Course : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class CourseWithCount : Course
    {
        public int TotalCount { get; set; }

    }
}
