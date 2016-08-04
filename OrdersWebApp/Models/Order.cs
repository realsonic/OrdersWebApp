using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OrdersWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
    }
}