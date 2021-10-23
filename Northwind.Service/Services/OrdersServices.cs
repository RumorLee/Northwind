using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.DomainLayer.Models;
using Northwind.DomainLayer.Services;
using Northwind.DomainLayer.Repositorys;
using Northwind.DomainLayer.Communication;

namespace Northwind.ServicesLayer.Services
{
    public class OrdersServices : IOrdersServices
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersServices(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Orders>> ListAsync()
        {
            return await _ordersRepository.ListAsync();
        }

        public async Task<Orders> FindAsync(int orderID)
        {
            return await _ordersRepository.FindAsync(orderID);
        }

        public async Task<Response<Orders>> AddAsync(Orders orders)
        {
            try
            {
                await _ordersRepository.AddAsync(orders);
                await _unitOfWork.CompleteAsync();

                return new Response<Orders>(orders);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Orders>($"An error occurred when saving the category: {ex.Message}");
            }
        }
    }
}
