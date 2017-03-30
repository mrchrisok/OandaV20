using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   class TradeReplaceExitOrdersErrorResponse : Response
   {
      public OrderCancelRejectTransaction takeProfitOrderCancelRejectTransaction;
      public TakeProfitOrderRejectTransaction takeProfitOrderRejectTransaction;
      public OrderCancelRejectTransaction takeProfitOrderCreatedCancelRejectTransaction;
      public OrderCancelRejectTransaction stopLossOrderCancelRejectTransaction;
      public StopLossOrderRejectTransaction stopLossOrderRejectTransaction;
      public OrderCancelRejectTransaction trailingStopLossOrderCancelRejectTransaction;
      public TrailingStopLossOrderRejectTransaction trailingStopLossOrderRejectTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
      public string errorCode { get; set; }
      public string errorMessage { get; set; }
   }
}
