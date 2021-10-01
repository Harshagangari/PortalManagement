using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalManagement.Entities
{
    public class PensionDetails
    {
        public enum PensionType { self, family }
        [Required(ErrorMessage = "Aadhaar Number is required")]
        [MaxLength(12, ErrorMessage = "Aadhaar Number must be 12 digits"), MinLength(12, ErrorMessage = "Aadhaar Number must be 12 digits")]
        public double AadharNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public PensionType pensionType { get; set; }
        [Required(ErrorMessage = "PAN is required")]
        [RegularExpression("^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$", ErrorMessage = "Invalid PAN Number")]
        public string PAN { get; set; }
    }
}
