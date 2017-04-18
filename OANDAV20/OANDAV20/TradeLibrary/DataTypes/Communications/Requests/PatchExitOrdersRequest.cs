using OANDAV20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests
{
   public class PatchExitOrdersRequest : Request
   {
      public TakeProfitDetails takeProfit { get; set; }
      public StopLossDetails stopLoss { get; set; }
      public TrailingStopLossDetails trailingStopLoss { get; set; }
   }
}
