using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.DataLayer.Resources;
using Northwind.DomainLayer.Models;
using Northwind.DomainLayer.Services;

namespace Northwind.ServicesLayer.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersServices _ordersServices;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrdersServices ordersServices, IMapper mapper, ILogger<OrdersController> logger)
        {
            _ordersServices = ordersServices;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Orders
        [HttpGet]
        public async Task<IEnumerable<OrdersResources>> GetOrdersAsync()
        {
            var orders = await _ordersServices.ListAsync();
            var resources = _mapper.Map<IEnumerable<Orders>, IEnumerable<OrdersResources>>(orders);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<OrdersResources> GetOrdersByIDAsync(int id)
        {
            var orders = await _ordersServices.FindAsync(id);
            var resources = _mapper.Map<Orders, OrdersResources>(orders);

            return resources;
        }

        //// GET: api/Orders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Orders>> GetOrders(int id)
        //{
        //    var orders = await _context.Orders.FindAsync(id);

        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }

        //    return orders;
        //}

        //// PUT: api/Orders/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrders(int id, Orders orders)
        //{
        //    if (id != orders.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(orders).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrdersExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Orders
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        //{
        //    _context.Orders.Add(orders);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrders", new { id = orders.OrderId }, orders);
        //}

        //// DELETE: api/Orders/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Orders>> DeleteOrders(int id)
        //{
        //    var orders = await _context.Orders.FindAsync(id);
        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Orders.Remove(orders);
        //    await _context.SaveChangesAsync();

        //    return orders;
        //}

        //private bool OrdersExists(int id)
        //{
        //    return _context.Orders.Any(e => e.OrderId == id);
        //}
    }
}
