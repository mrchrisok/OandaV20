using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradeCloseResponse : Response
   {
      public MarketOrderTransaction orderCreateTransaction;
      public OrderFillTransaction orderFillTransaction;
      public OrderCancelTransaction orderCancelTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
