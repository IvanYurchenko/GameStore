using System;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace PaymentWCFService.DataContracts
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class VisaPaymentInfo
    {
        [DataMember(IsRequired = true)]
        [NotNullValidator(MessageTemplate = "Card number is required.")]
        [StringLengthValidator(16, RangeBoundaryType.Inclusive, 16, RangeBoundaryType.Inclusive,
          MessageTemplate = "Card number should contain {3} characters.")]
        public string CardNumber { get; set; }
        
        [DataMember(IsRequired = true)]
        [RangeValidator(100, RangeBoundaryType.Inclusive, 999,
          RangeBoundaryType.Inclusive,
          MessageTemplate = "СVV number should be between {3} and {5}.")]
        public int Cvv { get; set; }

        [DataMember(IsRequired = true)]
        [RangeValidator(2015, RangeBoundaryType.Inclusive, 2099, RangeBoundaryType.Inclusive,
          MessageTemplate = "Expiration year should be between {3} and {5}.")]
        public int ExpirationYear { get; set; }

        [DataMember(IsRequired = true)]
        [RangeValidator(1, RangeBoundaryType.Inclusive, 12, RangeBoundaryType.Inclusive,
          MessageTemplate = "Expiration month should be between {3} and {5}.")]
        public int ExpirationMonth { get; set; }

        [DataMember(IsRequired = true)]
        [NotNullValidator(MessageTemplate = "Full name is required.")]
        public string FullName { get; set; }

        [DataMember(IsRequired = true)]
        public string PaymentPurpose { get; set; }

        [DataMember(IsRequired = true)]
        public decimal PaymentAmount { get; set; }

        [DataMember(IsRequired = true)]
        public Guid Token { get; set; }

        [DataMember(IsRequired = true)]
        public string Payee { get; set; }

        [DataMember]
        public string UserEmail { get; set; }

        [DataMember]
        public string UserPhoneNumber { get; set; }
    }
}