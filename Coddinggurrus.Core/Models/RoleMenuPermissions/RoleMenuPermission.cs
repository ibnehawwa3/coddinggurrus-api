using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Models.RoleMenuPermissions
{
    public class RoleMenuPermission
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long MenuId { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Access { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? Archived { get; set; }
        public DateTime? ArchivedOn { get; set; }
        public long? ArchivedBy { get; set; }
    }
}
