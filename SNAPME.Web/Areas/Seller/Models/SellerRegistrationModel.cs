using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Areas.Seller.Models
{
    public class SellerRegistrationModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Url]
        public string Website { get; set; }
        [Display(Name = "Business Type")]
        public string BusinessType { get; set; }
        [Display(Name = "Legal Business Name")]
        public string LegalBusinessName { get; set; }
        [Display(Name = "Trading Name")]
        public string TradingName { get; set; }
        [Display(Name = "Tax Identification Number")]
        public string TaxID { get; set; }
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Country Code")]
        public int CountryCode { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Message")]
        public string Comments { get; set; }
        public bool TermsAccepted { get; set; }
    }
}