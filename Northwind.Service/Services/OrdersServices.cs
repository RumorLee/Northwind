using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.DomainLayer.Models;
using Northwind.DomainLayer.Services;
using Northwind.DomainLayer.Repositorys;

namespace Northwind.ServicesLayer.Services
{
    public class OrdersServices : IOrdersServices
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersServices(IOrdersRepository ordersRepository)
        {
            this._ordersRepository = ordersRepository;
        }

        public async Task<IEnumerable<Orders>> ListAsync()
        {
            return await _ordersRepository.ListAsync();
        }

        public async Task<Orders> FindAsync(int orderID)
        {
            return await _ordersRepository.FindAsync(orderID);
        }

    }
}
