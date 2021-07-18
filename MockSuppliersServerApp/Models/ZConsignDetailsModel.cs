using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Models
{
    public class ZConsignDetailsModel
    {
        [Required(ErrorMessage = "Source is missing")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination is missing")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Packages is missing")]
        public int[] Packages { get; set; }
    }
}
