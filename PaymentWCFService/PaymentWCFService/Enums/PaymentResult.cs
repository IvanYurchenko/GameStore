namespace PaymentWCFService.Enums
{
    public enum PaymentResult
    {
        Success,

        NotEnoughMoney,

        CardDoesNotExist,
        
        PayeeDoesNotExist,

        WrongToken,

        Failure,
    }
}