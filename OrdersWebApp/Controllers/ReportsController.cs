using System.Linq;
using System.Web.Http;
using OrdersWebApp.Models;

namespace OrdersWebApp.Controllers
{
    public class ReportsController : ApiController
    {
        // GET: api/Reports/{id}
        public IHttpActionResult GetReport(string id)
        {
            switch (id?.ToLower())
            {
                case "total":
                    using (var db = new OrdersContext())
                    {
                        var customersCount = db.Customers.Count();
                        var ordersCount = db.Orders.Count();
                        var avgOrders = (float) ordersCount / customersCount;
                        var avgPrice = db.Orders.Average(o => o.Price);

                        return
                            Ok(
                                new
                                {
                                    CustomersCount = customersCount,
                                    OrdersCount = ordersCount,
                                    AvgOrders = avgOrders,
                                    AvgPrice = avgPrice
                                });
                    }
                default:
                    return BadRequest($"Unknown report named {id}");
            }
        }
    }
}