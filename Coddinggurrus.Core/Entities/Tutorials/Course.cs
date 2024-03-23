namespace Coddinggurrus.Core.Entities.Tutorials
{
    public class Course : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
    public class CourseWithCount : Course
    {
        public int TotalRecords { get; set; }
    }
}
