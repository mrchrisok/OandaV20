using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-ep
   /// </summary>
   public class OrderCancelReplaceResponse : Response
   {
      public OrderCancelTransaction orderCancelTransaction;
      public Transaction.Transaction orderCreateTransaction;
      public OrderFillTransaction orderFillTransaction;
      public Transaction.Transaction orderReissueTransaction;
      public Transaction.Transaction orderReissueRejectTransaction;
      public OrderCancelTransaction replacingOrderCancelTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
