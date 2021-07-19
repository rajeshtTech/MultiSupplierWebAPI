using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClientServiceApp.Models
{
    public class ZConsignDetailsModel: BaseConsignDetailsModel
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public int[] Packages { get; set; }
    }
}
