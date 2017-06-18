using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-ep
   /// </summary>
   public class OrderCancelReplaceResponse : Response
   {
      public OrderCancelTransaction orderCancelTransaction;
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction orderCreateTransaction;
      public OrderFillTransaction orderFillTransaction;
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction orderReissueTransaction;
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction orderReissueRejectTransaction;
      public OrderCancelTransaction replacingOrderCancelTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
