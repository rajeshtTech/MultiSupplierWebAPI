using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class XConsignDetailsModel
    {
        public string ContactAddress { get; set; }

        public string WarehouseAddress { get; set; }

        public int[] PackageDimensions { get; set; }
    }
}
