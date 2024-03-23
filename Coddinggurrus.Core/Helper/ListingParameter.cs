
namespace Coddinggurrus.Core.Helper
{
    public class ListingParameter
    {
        public string? TextToSearch { get; set; }
        public string? SortColumn { get; set; }
        public int Skip { get; set; } = 1;
        public int Take { get; set; } = 10;
        public bool SelilizationNeeded { get; set; } = true;
        public string SortOrder { get; set; } = string.Empty;
    }
}
