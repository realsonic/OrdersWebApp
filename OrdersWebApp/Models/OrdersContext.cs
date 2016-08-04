using System.Data.Entity;

namespace OrdersWebApp.Models
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}