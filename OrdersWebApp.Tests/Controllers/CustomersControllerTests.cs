using System.Collections.Generic;
using System.Threading;
using System.Web.Http.Results;
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

            var genType = result.GetType().GetGenericTypeDefinition();
            Assert.AreEqual(genType, typeof(OkNegotiatedContentResult<>),
                $"Result generic type is {genType}, but should be OkNegotiatedContentResult<>");
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