using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Models
{
    public class XConsignDetailsModel
    {
        [Required(ErrorMessage ="Contact Address is missing")]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "Warehouse Address is missing")]
        public string WarehouseAddress { get; set; }

        [Required(ErrorMessage = "Package Dimensions is missing")]
        public int[] PackageDimensions { get; set; }
    }
}
