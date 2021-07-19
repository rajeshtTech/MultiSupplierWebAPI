using ClientServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Repositories.WebAPIRepositories
{
    public interface ISuppConsignRepositoryBase
    {
        public Task<SupplierQuoteResponse> GetConsignmentQuote(BaseConsignDetailsModel consignDetails);
    }
}
