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
        public string NumOrder { get; set; }
        public StatusOrder Status { get; set; }
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
        public bool Paid { get; set; }

        public Guid? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
