using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class Stock
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }
        public int QuantitySaved { get; set; }
        public int QuantityMin { get; set; }
        public int QuantityMax { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
