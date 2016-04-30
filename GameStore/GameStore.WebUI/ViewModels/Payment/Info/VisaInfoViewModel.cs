using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Payment.Info
{
    public class VisaInfoViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [StringLength(16, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "SingleRangeValidationMessage",
            MinimumLength = 16)]
        [Display(ResourceType = typeof(GlobalRes), Name = "CardNumber")]
        public string CardNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Range(100, 999, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RangeValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Cvv")]
        public int Cvv { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Range(2015, 2099, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RangeValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "ExpirationYear")]
        public int ExpirationYear { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Range(1, 12, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RangeValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "ExpirationMonth")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "FullName")]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "PaymentPurpose")]
        public string PaymentPurpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "PaymentAmount")]
        public decimal PaymentAmount { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [HiddenInput]
        public Guid Token { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [HiddenInput]
        public string Payee { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Email")]
        public string UserEmail { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "PhoneNumber")]
        public string UserPhoneNumber { get; set; }
    }
}