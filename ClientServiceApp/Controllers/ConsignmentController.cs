using ClientServiceApp.BO;
using ClientServiceApp.Infrastructure.Helper;
using ClientServiceApp.Models;
using ClientServiceApp.Repositories.WebAPIRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClientServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsignmentController : ControllerBase
    {        
        IConsignmentBO _consignBO;
        public ConsignmentController(IConsignmentBO consignBO) 
        {
           _consignBO = consignBO;
        }

        [HttpGet]
        public async Task<IActionResult> GetLowestQuote([FromQuery] ConsginmentDetails consignDetails)
        {
           var suppQuoteResponse = await _consignBO.GetLowestConsignmentQuote(consignDetails);
           return GetResponseResult(suppQuoteResponse);              
        }

        #region PRIVATE METHODS
        private IActionResult GetResponseResult(BaseResponseObject responseObject)
        {
            if (responseObject.ResponseStatusCode == HttpStatusCode.OK)
                return Ok(responseObject);
            else if (responseObject.ResponseStatusCode == HttpStatusCode.NoContent)
                return NoContent();
            else if (responseObject.ResponseStatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(responseObject);
            else if (responseObject.ResponseStatusCode == HttpStatusCode.NotFound)
                return NotFound(responseObject);
            else
                return BadRequest(responseObject);
        }
        #endregion
    }
}
