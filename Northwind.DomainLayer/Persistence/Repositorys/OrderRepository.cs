using Northwind.DomainLayer.Models;
using Northwind.DomainLayer.Persistence.Contexts;
using Northwind.DomainLayer.Repositorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Northwind.DomainLayer.Persistence.Repositorys
{
    public class OrderRepository : BaseRepository, IOrdersRepository
    {
        public OrderRepository(NorthwindContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Orders>> ListAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Orders> FindAsync(int orderID)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(o => o.OrderId == orderID);
        }

        public async Task AddAsync(Orders orders)
        {
            await _context.Orders.AddAsync(orders);
        }


    }
}
