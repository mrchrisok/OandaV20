using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradePatchExitOrdersResponse : Response
   {
      public OrderCancelTransaction takeProfitOrderCancelTransaction { get; set; }
      public TakeProfitOrderTransaction takeProfitOrderTransaction { get; set; }
      public OrderFillTransaction takeProfitOrderFillTransaction { get; set; }
      public OrderCancelTransaction takeProfitOrderCreatedCancelTransaction { get; set; }
      public OrderCancelTransaction stopLossOrderCancelTransaction { get; set; }
      public StopLossOrderTransaction stopLossOrderTransaction { get; set; }
      public OrderFillTransaction stopLossOrderFillTransaction { get; set; }
      public OrderCancelTransaction stopLossOrderCreatedCancelTransaction { get; set; }
      public OrderCancelTransaction trailingStopLossOrderCancelTransaction { get; set; }
      public TrailingStopLossOrderTransaction trailingStopLossOrderTransaction { get; set; }
   }
}
