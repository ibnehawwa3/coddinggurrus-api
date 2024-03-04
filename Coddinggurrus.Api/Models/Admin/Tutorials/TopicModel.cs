namespace Coddinggurrus.Api.Models.Admin.Tutorials
{
    public class TopicModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public long CourseId { get; set; }
    }
}
