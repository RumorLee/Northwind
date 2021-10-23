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
    }
}
