using System;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using PaymentWCFService.DataContracts;
using PaymentWCFService.Enums;

namespace PaymentWCFService.ServiceContracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPaymentWcfService" in both code and config file together.
    [ServiceContract]
    [ValidationBehavior]
    public interface IPaymentWcfService
    {
        [OperationContract]
        Guid GetToken();

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        PaymentResult MakePayment(VisaPaymentInfo paymentInfo);
    }
}
