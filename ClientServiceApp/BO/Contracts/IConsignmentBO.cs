using ClientServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.BO
{
    public interface IConsignmentBO
    {
        public Task<SupplierQuoteResponse> GetLowestConsignmentQuote(ConsginmentDetails consignDetails);
    }
}
