using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreDemoProject.Models.Domain;
using WebApiCoreDemoProject.Models.DTO;
using WebApiCoreDemoProject.Repositories.IRepository;

namespace WebApiCoreDemoProject.Controllers
{
    [Route("api/Regions")]
    [ApiController]
   
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;   
        }
        #region CRUDMethods
        //[HttpGet]
        //public IActionResult GetAllRegions()
        //{
        //   var regions = _regionRepository.GetAll();//getting all regions from entity 
        //    var regionsDTO = new List<RegionDto>();//IEnumerable empty list
        //    regions.ToList().ForEach(region =>
        //    {
        //        var regionDTO = new RegionDto()
        //        {
        //            Id = region.Id,
        //            RegionCode = region.Code,
        //            AreaName = region.AreaName,
        //            Area = region.Area,
        //            Latitude = region.Latitude,
        //            Longitude = region.Longitude,
        //            Population = region.Population,
        //        };
        //        regionsDTO.Add(regionDTO);  //adding data to the list
        //    });
        //   return Ok(regionsDTO);
        //}

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetRegionsAsync()
        {
           var allRegions = await _regionRepository.GetAllAsync();
            var regionsDto = _mapper.Map<List<RegionDto>>(allRegions);
            return Ok(regionsDto);

        }

        [HttpGet]
        [Route("{id}")]//optional
        [ActionName("GetRegionByIdAsync")]
        [Authorize(Roles = "manager")]


        public async Task<IActionResult> GetRegionByIdAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var region= await _regionRepository.GetByIdAsync(id);
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //validate request
            if(!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                AreaName = addRegionRequest.AreaName,
                Area = addRegionRequest.Area,
                Latitude = addRegionRequest.Latitude,
                Longitude = addRegionRequest.Longitude,
                Population = addRegionRequest.Population,
            };
            region = await _regionRepository.AddRegionAsync(region);
            var regionDto = _mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetRegionByIdAsync),new { Id = regionDto.Id }, regionDto);
        }

        [HttpDelete]
        [Route("{id}")]//optional
        [Authorize(Roles = "admin")]


        public async Task<IActionResult> DeleteRegionAsync(int id)
        {
            //get region from db
            var region = await _regionRepository.DeleteRegionAsync(id);

            //check for null or undefined
            if(region == null)
            {
                return NotFound();
            }

            //convert response back to dto
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                AreaName = region.AreaName,
                Area = region.Area,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population,
            };
            //return ok
            return Ok(regionDto);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRegionAsync( [FromRoute]int id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
            if (!ValidateUpdateRegionAsync(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }
            //convert dto to domain model
            var region = new Region()
            {
                Code = updateRegionRequest.Code,
                AreaName = updateRegionRequest.AreaName,
                Area = updateRegionRequest.Area,
                Latitude = updateRegionRequest.Latitude,
                Longitude = updateRegionRequest.Longitude,
                Population = updateRegionRequest.Population,
            };
            //update the region using repo
            region = await _regionRepository.UpdateRegionAsync(id, region);

            //if null not found
            if (region == null)
            {
                return NotFound();
            }

            //convert domain back to dto 
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                AreaName = region.AreaName,
                Area = region.Area,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population,
            };
            // Or use this
            // var regionDto = _mapper.Map<RegionDto>(newRegion);

            //return ok reponse
            return Ok(regionDto);

        }
        #endregion

        #region PrivateMethods
        private bool ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"Add region data is mandatory");
                return false;
            }
            if(string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Code), $"{nameof(addRegionRequest.Code)} cannot be null or empty or whitespaces");
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.AreaName))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.AreaName), $"{nameof(addRegionRequest.AreaName)} cannot be null or empty or whitespaces");
            }
            if (addRegionRequest.Area <=0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Area), $"{nameof(addRegionRequest.Area)} cannot be less than or equal to Zero");
            }
            if (addRegionRequest.Latitude <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Latitude), $"{nameof(addRegionRequest.Latitude)} cannot be less than or equal to Zero");
            }
            if (addRegionRequest.Longitude == 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Longitude), $"{nameof(addRegionRequest.Longitude)} cannot be equal to Zero");
            }
            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Population), $"{nameof(addRegionRequest.Population)} cannot be less than Zero");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateRegionAsync(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), $"Add region data is mandatory");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Code), $"{nameof(updateRegionRequest.Code)} cannot be null or empty or whitespaces");
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.AreaName))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.AreaName), $"{nameof(updateRegionRequest.AreaName)} cannot be null or empty or whitespaces");
            }
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Area), $"{nameof(updateRegionRequest.Area)} cannot be less than or equal to Zero");
            }
            if (updateRegionRequest.Latitude <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Latitude), $"{nameof(updateRegionRequest.Latitude)} cannot be less than or equal to Zero");
            }
            if (updateRegionRequest.Longitude == 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Longitude), $"{nameof(updateRegionRequest.Longitude)} cannot be equal to Zero");
            }
            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Population), $"{nameof(updateRegionRequest.Population)} cannot be less than Zero");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
