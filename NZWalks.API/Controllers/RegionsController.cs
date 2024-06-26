﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            

            var regionsDomainModel = await regionRepository.GetAllAsync();
            //throw new Exception("region controller error");

            return Ok(mapper.Map<List<RegionDto>>(regionsDomainModel));
        
        }


        [HttpGet]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regions =await regionRepository.GetByIdAsync(id);
            if (regions == null)
            {
                return NotFound();
            }
            //var regionsDto = new RegionDto()
            //{
            //    Id = regions.Id,
            //    Name = regions.Name,
            //    Code = regions.Code,
            //    RegionImageUrl = regions.RegionImageUrl
            //};
            var regionsDto=mapper.Map<RegionDto>(regions);
            return Ok(regionsDto);
        }


        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionsDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionsDomainModel = await regionRepository.UpdateAsync(id, regionsDomainModel);
            if (regionsDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionsDomainModel);
            return Ok(regionDto);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)


            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
