using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MockSuppliersServerApp.Infrastructure.AppSettings;
using MockSuppliersServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierXController : ControllerBase
    {
        private AppSettings _appSettings;
        public SupplierXController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetConsignmentQuote([FromQuery] XConsignDetailsModel details)
        {
            var order  = _appSettings.OrderList.Where(order => order.Source == details.ContactAddress
                                                      && order.Destination == details.WarehouseAddress).FirstOrDefault();

            // decimal quoteAmt = order != null ? order.SuppXQuoteAmt : 4000M;

            if (order != null)
                return Ok(order.SuppXQuoteAmt);
            else
                return NoContent();
        }
    }
}
