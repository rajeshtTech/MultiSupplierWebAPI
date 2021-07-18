using ClientServiceApp.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientServiceApp.Infrastructure.Helper
{
    public interface IHttpHelper
    {
        public HttpClient GetHttpClientWithRequestHeader(string supplierName);
    }

    public class HttpHelper: IHttpHelper
    {
        private AppSettings _appSettings;        
        public HttpHelper(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;                     
        }
        public HttpClient GetHttpClientWithRequestHeader(string supplierName)
        {
            var httpClient = new HttpClient();
            var supplierDetails = _appSettings.SuppliersAPIList.Where(supplier => supplier.Name == supplierName).FirstOrDefault();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                                                           $"{supplierDetails.ClientId}:{supplierDetails.ClientCredentials}")));
            httpClient.DefaultRequestHeaders.Add(Constants.SUPPLIER, supplierDetails.Name);
            return httpClient;
        }
    }
}
