using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProiectWebData.Entities;
using ProiectWebService.Dtos;

namespace ProiectWebService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Items, ItemsDto>();
            CreateMap<ItemsDto, Items>();

            CreateMap<ServiceResponse<string>, ProiectWebData.ServiceResponse<string>>();
            CreateMap<ProiectWebData.ServiceResponse<string>, ServiceResponse<string>>();

        }
    }
}
