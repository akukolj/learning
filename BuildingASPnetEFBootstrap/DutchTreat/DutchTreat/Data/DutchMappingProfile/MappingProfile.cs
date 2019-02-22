using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Data.DutchMappingProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest=>dest.OrderId,p=>p.MapFrom(src=>src.Id))
                .ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
