using WebApiCoreDemoProject.Models.Domain;

namespace WebApiCoreDemoProject.Repositories.IRepository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(int id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(int id,Region region);
        Task<Region> DeleteRegionAsync(int  id);



    }
}
