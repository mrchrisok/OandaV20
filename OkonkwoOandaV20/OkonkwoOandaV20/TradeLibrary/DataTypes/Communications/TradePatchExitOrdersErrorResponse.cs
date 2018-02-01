using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradePatchExitOrdersErrorResponse : ErrorResponse
   {
      public OrderCancelRejectTransaction takeProfitOrderCancelRejectTransaction { get; set; }
      public TakeProfitOrderRejectTransaction takeProfitOrderRejectTransaction { get; set; }
      public OrderCancelRejectTransaction takeProfitOrderCreatedCancelRejectTransaction { get; set; }
      public OrderCancelRejectTransaction stopLossOrderCancelRejectTransaction { get; set; }
      public StopLossOrderRejectTransaction stopLossOrderRejectTransaction { get; set; }
      public OrderCancelRejectTransaction trailingStopLossOrderCancelRejectTransaction { get; set; }
      public TrailingStopLossOrderRejectTransaction trailingStopLossOrderRejectTransaction { get; set; }
   }
}
