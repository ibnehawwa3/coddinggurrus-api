
namespace Coddinggurrus.Core.Entities.Tutorials
{
    public class Topic : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long CourseId { get; set; }
        public string Tags { get; set; }
    }
    public class TopicCount : Topic
    {
        public int TotalRecords { get; set; }
    }
}
