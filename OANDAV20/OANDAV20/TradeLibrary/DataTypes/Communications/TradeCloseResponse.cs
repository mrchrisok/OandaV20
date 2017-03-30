using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
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
