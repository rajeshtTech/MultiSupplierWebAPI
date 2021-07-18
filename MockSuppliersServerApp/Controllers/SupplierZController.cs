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
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierZController : ControllerBase
    {
        private AppSettings _appSettings;
        public SupplierZController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public IActionResult GetConsignmentQuote([FromQuery] ZConsignDetailsModel details)
        {
            var order = _appSettings.OrderList.Where(order => order.Source == details.Source
                                                     && order.Destination == details.Destination).FirstOrDefault();

            //decimal quoteAmt = order != null ? order.SuppZQuoteAmt : 2000M;

            //return Ok(quoteAmt);
            if (order != null)
                return Ok(order.SuppZQuoteAmt);
            else
                return NoContent();
        }       
    }
}
