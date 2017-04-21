using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Reflection;
using System.Linq;

namespace OANDAV20.Framework.JsonConverters
{
   public class PriceObjectConverter : JsonConverterBase
   {
      public override bool CanConvert(Type objectType)
      {
         bool canConvert = objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IHasPrices));
         return canConvert;
      }

      public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
      {
         JObject jo = new JObject();
         Type type = value.GetType();

         if ((value as IHasPrices) != null)
         { 
            var priceObject = value as IHasPrices;
            var priceProperties = priceObject.priceInformation.priceProperties;
            var pricePrecision = priceObject.priceInformation.instrument.displayPrecision;

            foreach (PropertyInfo property in type.GetRuntimeProperties())
            {
               if (property.CanRead)
               {
                  object propertyValue = property.GetValue(value, null);

                  if (priceProperties.Contains(property.Name))
                  {
                     double price = Convert.ToDouble(propertyValue);
                     string formattedPrice = price.ToString("F" + pricePrecision, CultureInfo.InvariantCulture);

                     jo.Add(property.Name, JToken.FromObject(formattedPrice));
                  }
                  else
                  {
                     if (propertyValue == null && serializer.NullValueHandling == NullValueHandling.Ignore)
                        continue;
                     else if (property.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(JsonIgnoreAttribute)) != null)
                        continue;
                     else
                        jo.Add(property.Name, JToken.FromObject(propertyValue, serializer));
                  }
               }
            }

         }

         jo.WriteTo(writer);
      }
   }
}
