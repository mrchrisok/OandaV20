using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradeClientExtensionsModifyErrorResponse : ErrorResponse
   {
      public TradeClientExtensionsModifyRejectTransaction tradeClientExtensionsModifyRejectTransaction { get; set; }
   }
}
