using Arizona.Data.Entities;
using Arizona.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arizona.Data
{
    public class ArizonaMappingProfile : Profile
    {
        public ArizonaMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
              .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
              .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
              .ForMember(i => i.OrderItemId, ex => ex.MapFrom(vm => vm.Id))
              .ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ForMember(p => p.ProductId, ex => ex.MapFrom(vm => vm.Id));
        }
    }
}
