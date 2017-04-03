using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OANDAV20.TradeLibrary.DataTypes.Transaction;
using System;
using System.Collections.Generic;

namespace OANDAV20.Framework
{
   public class TransactionConverter : JsonConverter
   {
      public override bool CanWrite => false;
      public override bool CanRead => true;

      public override bool CanConvert(Type objectType)
      {
         return objectType == typeof(ITransaction);
      }

      public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
      {
         throw new InvalidOperationException("Use default serialization.");
      }

      public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
      {
         var jsonArray = JArray.Load(reader);
         var transactions = new List<ITransaction>();

         foreach (var item in jsonArray)
         {
            var transaction = TransactionFactory.Create(item["type"].Value<string>());
            serializer.Populate(item.CreateReader(), transaction);
            transactions.Add(transaction);
         }

         return transactions;
      }
   }
}
