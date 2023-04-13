using WebApiCoreDemoProject.Models.Domain;

namespace WebApiCoreDemoProject.Repositories.IRepository
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
