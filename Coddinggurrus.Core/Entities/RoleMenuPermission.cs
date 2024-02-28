
namespace Coddinggurrus.Core.Entities
{
    public class RoleMenuPermission : Entity<int>
    {
        public long Id { get; set; }
        public string RoleId { get; set; }
        public string? MenuName { get; set; }
        public long MenuId { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Access { get; set; }
    }
    public class RoleMenuPermissionWithCount : RoleMenuPermission
    {
        public int TotalCount { get; set; }

    }
}
