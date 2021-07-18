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
    public class SupplierYController : ControllerBase
    {
        private AppSettings _appSettings;
        public SupplierYController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult GetConsignmentQuote([FromQuery] YConsignDetailsModel details)
        {
            var order = _appSettings.OrderList.Where(order => order.Source == details.Consignee
                                                     && order.Destination == details.Consignor).FirstOrDefault();

            //decimal quoteAmt = order != null ? order.SuppYQuoteAmt : 6000M;

            //return Ok(quoteAmt);
            if (order != null)
                return Ok(order.SuppYQuoteAmt);
            else
                return NoContent();
        }
    }
}
