using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DomainLayer.Repositorys
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
