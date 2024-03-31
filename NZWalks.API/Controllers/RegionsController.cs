using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
           this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain= dbContext.Regions.ToList();
            var regionsDto = new List<RegionDto>();

            foreach(var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id=region.Id,
                    Name=region.Name,
                    Code=region.Code,
                    RegionImageUrl=region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regions = dbContext.Regions.FirstOrDefault(x=>x.Id==id);
            if (regions == null)
            {
                return NotFound();
            }
            var regionsDto = new RegionDto()
            {
                Id = regions.Id,
                Name = regions.Name,
                Code = regions.Code,
                RegionImageUrl = regions.RegionImageUrl
            };
            return Ok(regionsDto);
        }


        [HttpPost]
        public IActionResult Create([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id=regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionsDomainModel = dbContext.Regions.FirstOrDefault(x=>x.Id == id);
            if(regionsDomainModel == null)
            {
                return NotFound();
            }

            regionsDomainModel.Code = updateRegionRequestDto.Code;
            regionsDomainModel.Name = updateRegionRequestDto.Name;
            regionsDomainModel.RegionImageUrl= updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id=regionsDomainModel.Id,
                Code = regionsDomainModel.Code,
                Name = regionsDomainModel.Name,
                RegionImageUrl = regionsDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
