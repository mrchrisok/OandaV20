using Newtonsoft.Json;
using OANDAV20.Framework.JsonConverters;
using OANDAV20.TradeLibrary.DataTypes.Order;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class OrdersResponse
   {
      [JsonConverter(typeof(OrderConverter))]
      public List<IOrder> orders;
      public long lastTransactionID;
   }
}
