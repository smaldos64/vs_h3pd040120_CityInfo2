

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Profiles
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            // CreateMap<source, destination>()
            CreateMap<Entities.Language, Models.LanguageDto>();

            CreateMap<Models.LanguageDto, Entities.Language>();
        }
    }
}
