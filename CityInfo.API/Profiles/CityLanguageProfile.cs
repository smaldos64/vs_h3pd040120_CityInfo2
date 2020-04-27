using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Profiles
{
    public class CityLanguageProfile : Profile
    {
        public CityLanguageProfile()
        {
            // CreateMap<source, destination>()
            CreateMap<Entities.CityLanguage, Models.CityLanguageDto>();

            CreateMap<Models.CityLanguageDto, Entities.CityLanguage>();
        }
    }
}
