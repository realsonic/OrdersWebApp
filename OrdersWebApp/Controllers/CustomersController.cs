using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using OrdersWebApp.Models;

namespace OrdersWebApp.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly OrdersContext _db = new OrdersContext();

        /*// GET: api/Customers
        public JsonResult<List<Customer>> GetCustomers()
        {
            var data = _db.Customers.ToList();
            return Json(data,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new FlatCustomerContractResolver()
                });
        }*/

        // GET: api/Customers
        public IEnumerable<dynamic> GetCustomers()
        {
            return _db.Customers.Select(c => new {c.Id, c.Name, c.Email});
        }

/*
        private class FlatCustomerContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                if (property.PropertyName == nameof(Customer.Orders))
                {
                    property.ShouldSerialize = obj => false;
                }

                return property;
            }
        }
*/

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _db.Entry(customer).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Customers.Add(customer);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = customer.Id}, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            _db.Customers.Remove(customer);
            _db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return _db.Customers.Count(e => e.Id == id) > 0;
        }
    }
}