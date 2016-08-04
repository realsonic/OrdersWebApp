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
        private static readonly Customer JohnSmithCustomer = new Customer
        {
            Name = "John Smith",
            Email = "johnsmith@mail.ru",
            Orders = new List<Order> {new Order {Price = 1000, CreatedDate = DateTime.Now}}
        };

        private static readonly Customer JohnDoeCustomer = new Customer
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com"
        };

        [TestInitialize]
        public void Initialize()
        {
            // we are connected to local db
            using (var db = new OrdersContext())
            {
                // add John Smith
                db.Customers.Add(JohnSmithCustomer);
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
                result = controller.GetCustomer(JohnSmithCustomer.Id);
            }

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Customer>));
            var customer = (result as OkNegotiatedContentResult<Customer>)?.Content;
            Assert.IsNotNull(customer);
            Assert.AreEqual(JohnSmithCustomer.Id, customer.Id);
            Assert.AreEqual(JohnSmithCustomer.Name, customer.Name);
            Assert.AreEqual(JohnSmithCustomer.Email, customer.Email);
            Assert.AreEqual(JohnSmithCustomer.Orders.Count, customer.Orders.Count);
        }

        [TestMethod]
        public void PostCustomerTest()
        {
            IHttpActionResult result;
            using (var controller = new CustomersController())
            {
                result = controller.PostCustomer(JohnDoeCustomer);
            }

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<Customer>));
            var customer = (result as CreatedAtRouteNegotiatedContentResult<Customer>)?.Content;
            Assert.IsNotNull(customer);
            Assert.AreEqual(JohnDoeCustomer.Id, customer.Id);
            Assert.AreEqual(JohnDoeCustomer.Name, customer.Name);
            Assert.AreEqual(JohnDoeCustomer.Email, customer.Email);
        }

        [TestMethod]
        public void PostOrderTest()
        {
            IHttpActionResult result;
            using (var controller = new CustomersController())
            {
                //result = controller.PostOrder();
            }
        }
    }
}