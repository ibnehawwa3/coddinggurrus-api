namespace Coddinggurrus.Api.Models.Admin.RoleMenuPermission
{
    public class RoleMenuPermissionModel
    {
        public long Id { get; set; }
        public string RoleId { get; set; }
        public long MenuId { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Access { get; set; }
    }
}
