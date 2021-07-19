using ClientServiceApp.Infrastructure.Configuration;
using ClientServiceApp.Infrastructure.Helper;
using ClientServiceApp.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientServiceApp.Repositories.WebAPIRepositories.Services
{
    public abstract class SupplierBaseConsignRepository
    {
        private IHttpHelper _httpHelper;
        private AppSettings _appSettings;
        public SupplierBaseConsignRepository(IHttpHelper httpHelper, IOptions<AppSettings> appSettings)
        {
            _httpHelper = httpHelper;            
            _appSettings = appSettings.Value;
        }
        public async Task<HttpResponseMessage> GetQuoteBySupplierAndServiceName(string suplierName, string serviceName, string queryString)
        {
            var supplierAPIDetails = _appSettings.SuppliersAPIList.Where(supplier => supplier.Name == suplierName).FirstOrDefault();
            var serviceDetails = supplierAPIDetails.Services.Where(service => service.Name == serviceName).FirstOrDefault();

            return await _httpHelper.GetHttpClientWithRequestHeader(suplierName).GetAsync(supplierAPIDetails.BaseAddress + serviceDetails.Url + "?" + queryString);
        }
    }
}
