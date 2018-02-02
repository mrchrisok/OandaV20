using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class AccountConfigurationErrorResponse : ErrorResponse
   {
      public ClientConfigureRejectTransaction clientConfigureRejectTransaction { get; set; }
   }
}
