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
using System.Xml;

namespace ClientServiceApp.Repositories.WebAPIRepositories.Services
{
    public class SuppZConsignRepository : SupplierBaseConsignRepository, ISuppZConsignRepository
    {
        IUtility _utility;
        public SuppZConsignRepository(IUtility utility, IHttpHelper httpHelper, IOptions<AppSettings> appSettings) : base(httpHelper, appSettings)
        {
            _utility = utility;
        }

        public async Task<SupplierQuoteResponse> GetConsignmentQuote(ZConsignDetailsModel consignDetails)
        {
            string consignmentString = _utility.SearilizeObjToXML<ZConsignDetailsModel>(consignDetails);

            var response = await GetQuoteBySupplierAndServiceName(Constants.SUPPLIER_Z, Constants.GET_CONSIGNMENT_QUOTE, consignmentString);

            if (!response.IsSuccessStatusCode)
                return new SupplierQuoteResponse { Name = Constants.SUPPLIER_Z, ResponseStatusCode = response.StatusCode, ErrorResponse = response.ReasonPhrase };
            else if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new SupplierQuoteResponse { Name = Constants.SUPPLIER_Z, ResponseStatusCode = response.StatusCode };
            else
               {
                 XmlDocument quoteAmtXml = new XmlDocument();
                 quoteAmtXml.LoadXml(response.Content.ReadAsStringAsync().Result);
                 decimal.TryParse(quoteAmtXml.SelectSingleNode(Constants.DECIMAL).InnerText, out decimal quoteAmount);
                 return new SupplierQuoteResponse { Name = Constants.SUPPLIER_Z, QuoteAmount = quoteAmount, ResponseStatusCode = response.StatusCode};
               }
                
        }
    }
}
