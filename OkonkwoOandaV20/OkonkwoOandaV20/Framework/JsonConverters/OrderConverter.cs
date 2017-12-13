using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.Framework.JsonConverters
{
   public class OrderConverter : JsonConverterBase
   {
      public override bool CanConvert(Type objectType)
      {
         return objectType == typeof(IOrder);
      }

      public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
      {
         var jsonToken = JToken.Load(reader);

         if (jsonToken.Type == JTokenType.Array)
         {
            var orders = new List<IOrder>();

            var jsonArray = (JArray)jsonToken;

            foreach (var item in jsonArray)
            {
               var order = OrderFactory.Create(item["type"].Value<string>());
               serializer.Populate(item.CreateReader(), order);
               orders.Add(order);
            }

            return orders;
         }
         else if (jsonToken.Type == JTokenType.Object)
         {
            IOrder order = OrderFactory.Create(jsonToken["type"].Value<string>());
            serializer.Populate(jsonToken.CreateReader(), order);
            return order;
         }
         else
            throw new ArgumentException(string.Format("Unexpected JTokenType ({0}) in reader.", jsonToken.Type.ToString()));
      }
   }
}
