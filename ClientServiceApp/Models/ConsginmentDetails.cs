using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClientServiceApp.Models
{
    public class ConsginmentDetails
    {
        [Required(ErrorMessage = "Source is missing")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination is missing")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "PackageDimensions is missing")]
        public int[] PackageDimensions { get; set; }       
    }
}
