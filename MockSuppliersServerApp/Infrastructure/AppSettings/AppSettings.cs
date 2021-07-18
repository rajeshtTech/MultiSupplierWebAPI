using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Infrastructure.AppSettings
{
    public class AppSettings
    {
        public List<Quotation> OrderList { get;set;}
    }

    public class Quotation
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public decimal SuppXQuoteAmt { get; set; }
        public decimal SuppYQuoteAmt { get; set; }
        public decimal SuppZQuoteAmt { get; set; }
    }
}
