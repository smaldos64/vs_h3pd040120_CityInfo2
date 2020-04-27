using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesLazyLoadWithoutMapperController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesLazyLoadWithoutMapperController(ICityInfoRepository _cityInfoRepository,
                                                  IMapper _mapper)
        {
            this._cityInfoRepository = _cityInfoRepository;
            this._mapper = _mapper;
        }

        // GET: api/CitiesLazyLoadWithoutMapper
        [HttpGet]
        public IActionResult GetCities(bool includeRelations = false)
        {
            var cityEntities = _cityInfoRepository.GetCitiesAdvanced();

            if (false == includeRelations)
            {
                IEnumerable<CityWithoutPointsOfInterestDto> CityDtos = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);
                return Ok(CityDtos);
            }
            else
            {
                IEnumerable<CityDto> CityDtos = _mapper.Map<IEnumerable<CityDto>>(cityEntities);
                return Ok(CityDtos);
                //return Ok(_mapper.Map<IEnumerable<CityDto>>(cityEntities));
            }
        }

        // GET: api/CitiesLazyLoadWithoutMapper/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CitiesLazyLoadWithoutMapper
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/CitiesLazyLoadWithoutMapper/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
