using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class HeaderOrder
    {
        public Guid Id { get; set; }
        public int NumOrder { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public bool Paid { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string Town { get; set; }


        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<LineOrder> LineOrders { get; set; }
    }
}
