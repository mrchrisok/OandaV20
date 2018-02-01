using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-ep
   /// </summary>
   public class OrderCancelReplaceResponse : Response
   {
      public OrderCancelTransaction orderCancelTransaction { get; set; }
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction orderCreateTransaction { get; set; }
      public OrderFillTransaction orderFillTransaction { get; set; }
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction orderReissueTransaction { get; set; }
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction orderReissueRejectTransaction { get; set; }
      public OrderCancelTransaction replacingOrderCancelTransaction { get; set; }
   }
}
