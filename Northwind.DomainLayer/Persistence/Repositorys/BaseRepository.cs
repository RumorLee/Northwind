using Northwind.DomainLayer.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DomainLayer.Persistence.Repositorys
{
    public abstract class BaseRepository
    {
        protected readonly NorthwindContext _context;

        public BaseRepository(NorthwindContext context)
        {
            _context = context;
        }
    }
}
