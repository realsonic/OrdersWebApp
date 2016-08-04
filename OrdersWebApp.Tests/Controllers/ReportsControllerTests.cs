using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersWebApp.Controllers;
using OrdersWebApp.Models;

namespace OrdersWebApp.Tests.Controllers
{
    [TestClass]
    public class ReportsControllerTests
    {
        private static readonly IEnumerable<Customer> CustomersList = new List<Customer>
        {
            new Customer
            {
                Name = "John Init I",
                Email = "johninit1@mail.ru",
                Orders =
                    new List<Order>
                    {
                        new Order {Price = 150, CreatedDate = DateTime.Now},
                        new Order {Price = 956, CreatedDate = DateTime.Now.AddDays(-10)}
                    }
            },
            new Customer
            {
                Name = "John Init II",
                Email = "johninit2@gmail.com",
                Orders =
                    new List<Order>
                    {
                        new Order {Price = 123, CreatedDate = DateTime.Now},
                        new Order {Price = 4569, CreatedDate = DateTime.Now.AddMonths(-1)},
                        new Order {Price = 42, CreatedDate = DateTime.Now.AddDays(-3)}
                    }
            }
        };

        [TestInitialize]
        public void Initialize()
        {
            // we are connected to local db
            using (var db = new OrdersContext())
            {
                // add customers
                foreach (var customer in CustomersList)
                {
                    db.Customers.Add(customer);
                }
                db.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            // we are connected to local db
            using (var db = new OrdersContext())
            {
                // clean Customers, Orders will be cleaned cascaded
                foreach (var customer in db.Customers)
                {
                    db.Entry(customer).State = EntityState.Deleted;
                }
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void GetReportTotalTest()
        {
            using (var controller = new ReportsController())
            {
                var result = controller.GetReport("Total");
                Assert.IsNotNull(result);

                var report = (result as OkNegotiatedContentResult<dynamic>)?.Content;

                var customersCount = CustomersList.Count();
                var ordersCount = CustomersList.Sum(c => c.Orders.Count);
                var avgOrders = (float) ordersCount / customersCount;
                var avgPrice = CustomersList.SelectMany(c => c.Orders).Average(o => o.Price);

                var etalonReport = ReportsController.FormTotalReport(customersCount, ordersCount, avgOrders, avgPrice);

                Assert.AreEqual(etalonReport, report);
            }
        }
    }
}