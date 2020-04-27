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
    public class CitiesWithoutMapperController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesWithoutMapperController(ICityInfoRepository _cityInfoRepository,
                                             IMapper _mapper)
        {
            this._cityInfoRepository = _cityInfoRepository;
            this._mapper = _mapper;
        }

        // GET: api/CitiesWithoutMapper
        [HttpGet]
        public IActionResult GetCities(bool includeRelations = false)
        {
            var cityEntities = _cityInfoRepository.GetCitiesAdvanced();

            return Ok(cityEntities);
        }
    }
}
