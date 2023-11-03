using AutoMapper;
using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Service.MapProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Carriers, ResponseCarriersDTO>().ReverseMap();
            CreateMap<Carriers, RequestCarriersDTO>().ReverseMap();
            CreateMap<CarrierConfigurations, ResponseCarrierConfigurationsDTO>().ReverseMap();
            CreateMap<CarrierConfigurations, RequestCarrierConfigurationsDTO>().ReverseMap();
            CreateMap<Orders, ResponseOrdersDTO>().ReverseMap();
            CreateMap<Orders, RequestOrdersDTO>().ReverseMap();
        }
    }
}
