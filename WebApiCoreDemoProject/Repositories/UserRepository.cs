using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApiCoreDemoProject.Data;
using WebApiCoreDemoProject.Models.Domain;
using WebApiCoreDemoProject.Repositories.IRepository;

namespace WebApiCoreDemoProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WalksDbContext _db;
        public UserRepository(WalksDbContext db)
        {
            _db=db;
        }
        public async Task<User> AuthenticateUserAsync(string userName, string password)
        {
           var findUser = await  _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password);
            if (findUser == null)
            {
                return null;
            }
            var userRoles = await _db.User_Roles.Where(x=>x.UserId == findUser.Id).ToListAsync();
            if(userRoles.Any())
            {
                findUser.Roles = new List<string>();//collection of roles for that user which has to be found
                foreach(var userRole in userRoles)
                {
                   var roleName =  await _db.Roles.FirstOrDefaultAsync(x=>x.Id == userRole.RoleId);
                    if(roleName != null)
                    {
                        findUser.Roles.Add(roleName.RoleName);
                    }
                }
            }
            findUser.Password = null;
            return findUser;
        }
        public async Task<List<string>> GetUserRolesAsync(string userName, string password)
        {
            var findUser = await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password);
            if (findUser == null)
            {
                return null;
            }
            List<string> roles = new List<string>();//collection of roles for that user which has to be found
            var userRoles = await _db.User_Roles.Where(x => x.UserId == findUser.Id).ToListAsync();
            if (userRoles.Any())
            {
                foreach (var userRole in userRoles)
                {
                    var roleName = await _db.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (roleName != null)
                    {
                        roles.Add(roleName.RoleName);
                    }
                }
            }
            
            return roles;
        }
    }
}
