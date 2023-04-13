using WebApiCoreDemoProject.Models.Domain;
using WebApiCoreDemoProject.Repositories.IRepository;

namespace WebApiCoreDemoProject.Repositories
{
    public class UserStaticRepository // : IUserRepository
    {
        private List<User> users = new List<User>()
        {
            //new User()
            //{
            //    FirstName="User1",
            //    LastName="User",
            //    EmailAddress="user1@user.com",
            //    Id=1,
            //    UserName="user1@user.com",
            //    Password="user1",
            //    Roles=new List<string> {"manager"}
            //},
            //new User()
            //{
            //    FirstName="User2",
            //    LastName="User",
            //    EmailAddress="user2@user.com",
            //    Id=1,
            //    UserName="user2@user.com",
            //    Password="user2",
            //    Roles=new List<string> {"admin","manager"}
            //},
        };
        public async Task<User> AuthenticateUserAsync(string userName, string password)
        {
           var checkUser =  users.Find(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) && u.Password.Equals(password));
            return checkUser;
        }
    }
}
