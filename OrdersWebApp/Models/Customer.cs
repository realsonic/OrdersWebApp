using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrdersWebApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}