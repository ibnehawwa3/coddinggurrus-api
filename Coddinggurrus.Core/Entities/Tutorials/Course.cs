namespace Coddinggurrus.Core.Entities.Tutorials
{
    public class Course : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class CourseWithCount : Course
    {
        public int TotalRecords { get; set; }
    }
}
