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
    public class CitiesLazyLoadWithMapperController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesLazyLoadWithMapperController(ICityInfoRepository _cityInfoRepository,
                                                  IMapper _mapper)
        {
            this._cityInfoRepository = _cityInfoRepository;
            this._mapper = _mapper;
        }

        // GET: api/CitiesLazyLoadWithMapper
        [HttpGet]
        public IActionResult GetCities(bool includeRelations = false)
        {
            var cityEntities = _cityInfoRepository.GetCitiesAdvancedLazyLoad();

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
    }
}
