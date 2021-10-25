using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.DomainLayer.Resources;
using Northwind.DomainLayer.Models;
using Northwind.DomainLayer.Services;
using Microsoft.AspNetCore.Authorization;

namespace Northwind.ServicesLayer.Controller
{
    [Authorize]
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
            var orders = await _ordersServices.FindByIdAsync(id);
            var resources = _mapper.Map<Orders, OrdersResources>(orders);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveOrdersResources resource)
        {
            var orders = _mapper.Map<SaveOrdersResources, Orders>(resource);
            var result = await _ordersServices.AddAsync(orders);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Orders, OrdersResources>(result.Data);
            return Ok(resources);
        }

        /// <response code="400">Update failed</response>       
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrdersResources resource)
        {
            var orders = _mapper.Map<SaveOrdersResources, Orders>(resource);
            var result = await _ordersServices.UpdateAsync(id, orders);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Orders, OrdersResources>(result.Data);
            return Ok(resources);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ordersServices.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Orders, OrdersResources>(result.Data);
            return Ok(resources);
        }
    }
}
