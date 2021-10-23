using AutoMapper;
using Northwind.DomainLayer.Resources;
using Northwind.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DomainLayer.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Orders, OrdersResources>();
        }
    }
}
