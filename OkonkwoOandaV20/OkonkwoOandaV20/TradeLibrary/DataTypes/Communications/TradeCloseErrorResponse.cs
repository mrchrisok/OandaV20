using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradeCloseErrorResponse : ErrorResponse
   {
      public MarketOrderRejectTransaction orderRejectTransaction { get; set; }
   }
}
