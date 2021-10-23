using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Northwind.DomainLayer.Models;

namespace Northwind.DomainLayer.Services
{
    public interface IOrdersServices
    {
        Task<IEnumerable<Orders>> ListAsync();
        Task<Orders> FindAsync(int orderID);
    }
}
