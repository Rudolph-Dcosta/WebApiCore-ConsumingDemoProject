using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiCoreDemoProject.Data;
using WebApiCoreDemoProject.Models.Domain;
using WebApiCoreDemoProject.Repositories.IRepository;

namespace WebApiCoreDemoProject.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalksDbContext _walksDbContext ;
        private readonly IMapper _mapper ;
        public RegionRepository(WalksDbContext walksDbContext,IMapper mapper)
        {
            _walksDbContext = walksDbContext;
            _mapper = mapper;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            await _walksDbContext.Regions.AddAsync(region);
            await _walksDbContext.SaveChangesAsync();
            return region;
        
        }

        public async Task<Region> DeleteRegionAsync(int id)
        {
          var  region =  await _walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            _walksDbContext.Regions.Remove(region);
            await _walksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            var allRegions =await _walksDbContext.Regions.ToListAsync();//.ToList();
            return allRegions;
        }

        public async Task<Region> GetByIdAsync(int id)
        {
            return await _walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<Region> UpdateRegionAsync(int id, Region region)
        {
           var existingRegion = await _walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Area = region.Area;
            existingRegion.AreaName = region.AreaName;
            existingRegion.Latitude = region.Latitude;
            existingRegion.Longitude = region.Longitude;
            existingRegion.Population = region.Population; 
            await _walksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
