using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Northwind.DomainLayer.Resources;
using Northwind.DomainLayer.Models;

namespace Northwind.DomainLayer.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<AddOrdersResources, Orders>();
        }
    }
}
