using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Infrastructure.Configuration
{
    public class AppSettings
    {
        public List<SupplierAPIDetails> SuppliersAPIList { get; set; }
    }

    public class SupplierAPIDetails
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ClientCredentials { get; set; }
        public string BaseAddress { get; set; }
        public List<ServiceDetails> Services { get; set; }
    }

    public class ServiceDetails
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
