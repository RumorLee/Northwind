using AutoMapper;
using Northwind.DataLayer.Resources;
using Northwind.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DataLayer.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Orders, OrdersResources>();
        }
    }
}
