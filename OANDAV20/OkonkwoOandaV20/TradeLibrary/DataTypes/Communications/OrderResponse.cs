using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class OrderResponse
   {
      [JsonConverter(typeof(OrderConverter))]
      public IOrder order;
      public long lastTransactionID;
   }
}
