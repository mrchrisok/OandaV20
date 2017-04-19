using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OANDAV20.TradeLibrary.DataTypes.Stream;
using OANDAV20.TradeLibrary.DataTypes.Transaction;
using System;
using System.Collections.Generic;

namespace OANDAV20.Framework.JsonConverters
{
   public class TransactionStreamResponseConverter : JsonConverterBase
   {
      public override bool CanConvert(Type objectType)
      {
         bool canConvert = objectType.GetInterface("IHeartbeat") != null;
         return canConvert;
      }

      public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
      {
         TransactionStreamResponse response = new TransactionStreamResponse();

         var jsonToken = JToken.Load(reader);

         if (jsonToken.Type == JTokenType.Array)
         {
            var transactions = new List<ITransaction>();

            var jsonArray = (JArray)jsonToken;

            foreach (var item in jsonArray)
            {
               var transaction = TransactionFactory.Create(item["type"].Value<string>());
               serializer.Populate(item.CreateReader(), transaction);
               transactions.Add(transaction);
            }

            return transactions;
         }
         else if (jsonToken.Type == JTokenType.Object)
         {
            bool isHeartbeat = jsonToken["type"].Value<string>() == "HEARTBEAT";

            if (isHeartbeat)
            {
               var heartbeat = new TransactionHeartbeat();
               serializer.Populate(jsonToken.CreateReader(), heartbeat);
               response.heartbeat = heartbeat;
            }
            else
            {
               ITransaction transaction = TransactionFactory.Create(jsonToken["type"].Value<string>());
               serializer.Populate(jsonToken.CreateReader(), transaction);
               response.transaction = transaction;
            }

            return response;
         }
         else
            throw new ArgumentException(string.Format("Unexpected JTokenType ({0}) in reader.", jsonToken.Type.ToString()));
      }
   }
}
