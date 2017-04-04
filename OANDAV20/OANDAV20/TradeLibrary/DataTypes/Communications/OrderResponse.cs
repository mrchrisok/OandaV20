using Newtonsoft.Json;
using OANDAV20.Framework.JsonConverters;
using OANDAV20.TradeLibrary.DataTypes.Order;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class OrderResponse
   {
      [JsonConverter(typeof(OrderConverter))]
      public IOrder order;
      public long lastTransactionID;
   }
}
