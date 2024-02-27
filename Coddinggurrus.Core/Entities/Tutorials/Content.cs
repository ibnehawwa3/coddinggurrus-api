
namespace Coddinggurrus.Core.Entities.Tutorials
{
    public class Content : Entity<int>
    {
        public long TopicId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
    public class ContentWithCount : Content
    {
        public int TotalRecords { get; set; }
    }
}
