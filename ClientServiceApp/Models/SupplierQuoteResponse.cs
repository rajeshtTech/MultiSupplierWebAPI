using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class SupplierQuoteResponse: BaseResponseObject
    {
        public string Name { get; set; }
        public decimal QuoteAmount { get; set; }
          
    }
}
