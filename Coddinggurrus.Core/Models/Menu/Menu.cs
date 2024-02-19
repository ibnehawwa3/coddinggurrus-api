using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Models.Menu
{
    public class Menu
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long? ParentId { get; set; }
        public byte? MenuOrder { get; set; }
        public string MenuImage { get; set; }
        public int Status { get; set; }
        public string ApiPath { get; set; }
        public string Caption { get; set; }
        public bool IsShow { get; set; }
        public bool? IsSupperUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool Archived { get; set; }
        public DateTime? ArchivedOn { get; set; }
        public long? ArchivedBy { get; set; }
        public bool? IsSupperUserMenu { get; set; }
    }
}
