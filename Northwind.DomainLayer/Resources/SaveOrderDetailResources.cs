using Northwind.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DomainLayer.Resources
{
    public class SaveOrderDetailResources
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}
