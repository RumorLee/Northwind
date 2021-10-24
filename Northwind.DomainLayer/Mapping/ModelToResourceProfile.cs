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
            CreateMap<OrderDetails, OrderDetailResources>()
                .ForMember(odr => odr.ProductName, opt => opt.MapFrom(order => order.Product == null ? "" : order.Product.ProductName))
                ;

            CreateMap<Orders, OrdersResources>()
                .ForMember(or => or.CustomerName, opt => opt.MapFrom(order => order.Customer == null ? "" : order.Customer.CompanyName))
                .ForMember(or => or.EmployeeName, opt => opt.MapFrom(order => order.Employee == null ? "" : order.Employee.FirstName + " " + order.Employee.LastName))
                ;
        }
    }
}
