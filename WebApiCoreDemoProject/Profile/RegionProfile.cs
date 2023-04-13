using WebApiCoreDemoProject.Models.Domain;
using WebApiCoreDemoProject.Models.DTO;

namespace WebApiCoreDemoProject.Profile
{
    public class RegionProfile: AutoMapper.Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();

        }
    }
}
