using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Helper
{
    public class ListingParameter
    {
        public string? TextToSearch { get; set; }
        public string? SortColumn { get; set; }
        public int Skip { get; set; } = 1;
        public int Take { get; set; } = 10;
        public string SortOrder { get; set; } = string.Empty;
    }
}
