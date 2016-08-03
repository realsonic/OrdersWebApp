using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}