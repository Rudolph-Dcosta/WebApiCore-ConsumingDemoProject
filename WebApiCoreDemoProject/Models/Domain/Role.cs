using System.ComponentModel.DataAnnotations;

namespace WebApiCoreDemoProject.Models.Domain
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<User_Role> UserRoles { get; set; }

    }
}
