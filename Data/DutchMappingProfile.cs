using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchMappingProfile: Profile
    {
        public DutchMappingProfile()
        {
            //create a map between Order and OrderViewModel - try to match properties between them type to type
            CreateMap<Order, OrderViewModel>()
            .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
            .ReverseMap();


            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }     
    }
}
