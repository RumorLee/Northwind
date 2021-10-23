﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Northwind.DomainLayer.Models;
using Northwind.DomainLayer.Communication;

namespace Northwind.DomainLayer.Services
{
    public interface IOrdersServices
    {
        Task<IEnumerable<Orders>> ListAsync();
        Task<Orders> FindAsync(int orderID);
        Task<Response<Orders>> AddAsync(Orders orders);
    }
}
