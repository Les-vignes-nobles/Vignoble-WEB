using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class LineOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int QuantitySupplied { get; set; }
        public int HeaderOrderId { get; set; }
        public HeaderOrder HeaderOrder { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
