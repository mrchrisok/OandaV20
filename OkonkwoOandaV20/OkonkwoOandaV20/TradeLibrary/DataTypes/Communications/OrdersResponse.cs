using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class OrdersResponse : Response
   {
      [JsonConverter(typeof(OrderConverter))]
      public List<IOrder> orders { get; set; }
   }
}
