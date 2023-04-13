using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCoreDemoProject.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Navigation property

        public List<User_Role> UserRoles { get; set; }
    }
}
