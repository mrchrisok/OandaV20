using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class PositionCloseErrorResponse : ErrorResponse
   {
      public MarketOrderRejectTransaction longOrderRejectTransaction { get; set; }
      public MarketOrderRejectTransaction shortOrderRejectTransaction { get; set; }
   }
}
