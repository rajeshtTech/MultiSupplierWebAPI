using ClientServiceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ClientServiceApp.Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ClientServiceApp.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace ClientServiceApp.Repositories.WebAPIRepositories.Services
{
    public class SuppYConsignRepository : SupplierBaseConsignRepository, ISuppYConsignRepository
    {
        public SuppYConsignRepository(IHttpHelper httpHelper, IOptions<AppSettings> appSettings) : base(httpHelper, appSettings)
        {
        }

        public async Task<SupplierQuoteResponse> GetConsignmentQuote(BaseConsignDetailsModel consignDetails)
        {
            string consignmentString = HttpUtility.UrlEncode(JsonConvert.SerializeObject(consignDetails));

            var response = await GetQuoteBySupplierAndServiceName(Constants.SUPPLIER_Y, Constants.GET_CONSIGNMENT_QUOTE, consignmentString);

            if (!response.IsSuccessStatusCode)
                return new SupplierQuoteResponse { Name = Constants.SUPPLIER_Y, ResponseStatusCode = response.StatusCode, ErrorResponse = response.ReasonPhrase };
            else
            {
                decimal.TryParse(response.Content.ReadAsStringAsync().Result, out decimal quoteAmont);
                return new SupplierQuoteResponse { Name = Constants.SUPPLIER_Y, QuoteAmount = quoteAmont, ResponseStatusCode = response.StatusCode };
            }
        }
    }
}
