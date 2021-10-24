using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.DomainLayer.Models;
using Northwind.DomainLayer.Services;
using Northwind.DomainLayer.Repositorys;
using Northwind.DomainLayer.Communication;
using System.Reflection;

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

        public async Task<Orders> FindByIdAsync(int orderID)
        {
            return await _ordersRepository.FindByIdAsync(orderID);
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
                return new Response<Orders>($"An error occurred when saving the orders: {ex.Message}");
            }
        }

        public async Task<Response<Orders>> UpdateAsync(int orderID, Orders orders)
        {
            try
            {
                var existingOrders = await _ordersRepository.FindByIdAsync(orderID);

                if (existingOrders == null)
                    return new Response<Orders>("Orders not found.");

                foreach (PropertyInfo prop in typeof(Orders).GetProperties())
                {
                    if (!prop.CanWrite || !prop.CanRead || prop.Name == nameof(Orders.OrderId) || prop.Name == nameof(Orders.OrderDetails))
                        continue;

                    var updateValue = prop.GetValue(orders);

                    if (updateValue != null)
                        prop.SetValue(existingOrders, updateValue);
                }

                if (orders.OrderDetails.Count > 0)
                {
                    existingOrders.OrderDetails.Clear();

                    foreach (OrderDetails orderDetails in orders.OrderDetails)
                        existingOrders.OrderDetails.Add(orderDetails);
                }

                _ordersRepository.Update(existingOrders);

                await _unitOfWork.CompleteAsync();

                return new Response<Orders>(existingOrders);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Orders>($"An error occurred when updating the orders: {ex.Message}");
            }
        }

        public async Task<Response<Orders>> DeleteAsync(int orderID)
        {
            var existingCategory = await _ordersRepository.FindByIdAsync(orderID);

            if (existingCategory == null)
                return new Response<Orders>("Orders not found.");

            try
            {
                _ordersRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new Response<Orders>(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Orders>($"An error occurred when deleting the orders: {ex.Message}");
            }
        }
    }
}
