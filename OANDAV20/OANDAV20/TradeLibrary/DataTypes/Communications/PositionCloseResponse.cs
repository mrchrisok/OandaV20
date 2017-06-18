using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
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
