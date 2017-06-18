using Newtonsoft.Json;
using System;

namespace OkonkwoOandaV20.Framework.JsonConverters
{
   public abstract class JsonConverterBase : JsonConverter
   {
      public override bool CanWrite => true;
      public override bool CanRead => true;

      public override bool CanConvert(Type objectType)
      {
         throw new NotImplementedException("Inheriting class must implement.");
      }

      public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
      {
         throw new InvalidOperationException("Use default serialization.");
      }

      public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
      {
         throw new NotImplementedException("Inheriting class must implement.");
      }
   }
}
