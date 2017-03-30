using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   public class PositionCloseResponse : Response
   {
      public MarketOrderTransaction longOrderCreateTransaction;
      public OrderFillTransaction longOrderFillTransaction;
      public OrderCancelTransaction longOrderCancelTransaction;
      public MarketOrderTransaction shortOrderCreateTransaction;
      public OrderFillTransaction shortOrderFillTransaction;
      public OrderCancelTransaction shortOrderCancelTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
