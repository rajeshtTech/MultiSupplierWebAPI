using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class YConsignDetailsModel: BaseConsignDetailsModel
    {
        public string Consignee { get; set; }

        public string Consignor { get; set; }

        public int[] Cartons { get; set; }
    }
}
