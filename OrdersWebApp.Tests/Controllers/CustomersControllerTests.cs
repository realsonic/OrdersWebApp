using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersWebApp.Controllers;
using OrdersWebApp.Models;

namespace OrdersWebApp.Tests.Controllers
{
    [TestClass]
    public class CustomersControllerTests
    {
        [TestMethod]
        public void GetCustomersTest()
        {
            var controller = new CustomersController();
            var result = controller.GetCustomers();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetCustomerTest()
        {


            Assert.Fail();
        }

        [TestMethod]
        public void PutCustomerTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PostCustomerTest()
        {
            var controller = new CustomersController();
            var result = controller.PostCustomer(new Customer
            {
                Name = "John Smith",
                Email = "johnsmith@gmail.com"
            });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteCustomerTest()
        {
            Assert.Fail();
        }
    }
}