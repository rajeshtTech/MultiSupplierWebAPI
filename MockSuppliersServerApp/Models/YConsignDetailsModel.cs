using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Models
{
    public class YConsignDetailsModel
    {
        [Required(ErrorMessage = "Consignee is missing")]
        public string Consignee { get; set; }

        [Required(ErrorMessage = "Consignor is missing")]
        public string Consignor { get; set; }

        [Required(ErrorMessage = "Cartons is missing")]
        public int[] Cartons { get; set; }
    }
}
