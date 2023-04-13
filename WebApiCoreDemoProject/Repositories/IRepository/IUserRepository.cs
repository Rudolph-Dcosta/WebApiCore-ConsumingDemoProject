using WebApiCoreDemoProject.Models.Domain;

namespace WebApiCoreDemoProject.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string userName, string password);
        Task<List<string>> GetUserRolesAsync(string userName, string password);

    }
}
