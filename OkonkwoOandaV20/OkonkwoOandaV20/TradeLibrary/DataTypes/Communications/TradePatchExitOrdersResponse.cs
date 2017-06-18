using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradePatchExitOrdersResponse : Response
   {
      public OrderCancelTransaction takeProfitOrderCancelTransaction;
      public TakeProfitOrderTransaction takeProfitOrderTransaction;
      public OrderFillTransaction takeProfitOrderFillTransaction;
      public OrderCancelTransaction takeProfitOrderCreatedCancelTransaction;
      public OrderCancelTransaction stopLossOrderCancelTransaction;
      public StopLossOrderTransaction stopLossOrderTransaction;
      public OrderFillTransaction stopLossOrderFillTransaction;
      public OrderCancelTransaction stopLossOrderCreatedCancelTransaction;
      public OrderCancelTransaction trailingStopLossOrderCancelTransaction;
      public TrailingStopLossOrderTransaction trailingStopLossOrderTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
