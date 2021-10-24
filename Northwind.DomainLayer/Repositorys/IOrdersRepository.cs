using Northwind.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DomainLayer.Repositorys
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Orders>> ListAsync();
        Task<Orders> FindByIdAsync(int orderID);
        Task AddAsync(Orders orders);
        void Update(Orders orders);
        void Remove(Orders orders);
    }
}
