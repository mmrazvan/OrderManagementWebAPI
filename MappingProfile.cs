using AutoMapper;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;

namespace OrderManagementWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Labels, CreateUpdateLabels>().ReverseMap(); 
            CreateMap<Orders, CreateUpdateOrders>().ReverseMap(); 
        }
    }
}
