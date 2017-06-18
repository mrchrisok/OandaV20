using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-ep
   /// </summary>
   public class OrderCancelResponse : Response
   {
      public OrderCancelTransaction orderCancelTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
