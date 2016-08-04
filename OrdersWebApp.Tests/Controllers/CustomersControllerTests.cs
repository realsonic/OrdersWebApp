using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersWebApp.Controllers;
using OrdersWebApp.Models;

namespace OrdersWebApp.Tests.Controllers
{
    [TestClass]
    public class CustomersControllerTests
    {
        private static readonly Customer JohnInitCustomer = new Customer
        {
            Name = "John Init",
            Email = "johninit@mail.ru",
            Orders = new List<Order> {new Order {Price = 1000, CreatedDate = DateTime.Now}}
        };

        private static readonly Customer JohnPostCustomer = new Customer
        {
            Name = "John Post",
            Email = "johnpost@gmail.com"
        };

        private static readonly Order JohnInitPostOrder = new Order
        {
            Price = 2000,
            CreatedDate = DateTime.Now.AddDays(-5)
        };

        [TestInitialize]
        public void Initialize()
        {
            // we are connected to local db
            using (var db = new OrdersContext())
            {
                // add John Smith
                db.Customers.Add(JohnInitCustomer);
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
        public void GetCustomersTest()
        {
            using (var controller = new CustomersController())
            {
                var result = controller.GetCustomers();
                Assert.IsNotNull(result);
                Assert.AreEqual(GetCustomersCountFromDb(), result.Count());
            }
        }

        private static int GetCustomersCountFromDb()
        {
            using (var db = new OrdersContext())
            {
                return db.Customers.Count();
            }
        }

        [TestMethod]
        public void GetCustomerTest()
        {
            IHttpActionResult result;
            using (var controller = new CustomersController())
            {
                result = controller.GetCustomer(JohnInitCustomer.Id);
            }

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Customer>));
            var customer = (result as OkNegotiatedContentResult<Customer>)?.Content;
            Assert.IsNotNull(customer);
            Assert.AreEqual(JohnInitCustomer.Id, customer.Id);
            Assert.AreEqual(JohnInitCustomer.Name, customer.Name);
            Assert.AreEqual(JohnInitCustomer.Email, customer.Email);
            Assert.AreEqual(JohnInitCustomer.Orders?.Count, customer.Orders?.Count);
        }

        [TestMethod]
        public void PostCustomerTest()
        {
            IHttpActionResult result;
            using (var controller = new CustomersController())
            {
                result = controller.PostCustomer(JohnPostCustomer);
            }

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<Customer>));
            var customer = (result as CreatedAtRouteNegotiatedContentResult<Customer>)?.Content;
            Assert.IsNotNull(customer);
            Assert.AreEqual(JohnPostCustomer.Id, customer.Id);
            Assert.AreEqual(JohnPostCustomer.Name, customer.Name);
            Assert.AreEqual(JohnPostCustomer.Email, customer.Email);
        }

        [TestMethod]
        public void PostOrderTest()
        {
            IHttpActionResult result;
            using (var controller = new CustomersController())
            {
                result = controller.PostOrder(JohnInitCustomer.Id, JohnInitPostOrder);
            }

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<Order>));
            var order = (result as CreatedAtRouteNegotiatedContentResult<Order>)?.Content;
            Assert.IsNotNull(order);
            Assert.AreEqual(JohnInitPostOrder.Id, order.Id);
            Assert.AreEqual(JohnInitPostOrder.Price, order.Price);
            Assert.AreEqual(JohnInitPostOrder.CreatedDate, order.CreatedDate);
        }
    }
}