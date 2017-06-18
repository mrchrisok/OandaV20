using System.Reflection;
using System.Text;
using OANDACommon = OkonkwoOandaV20.Framework.Common;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class Response
   {
      public override string ToString()
      {
         // use reflection to display all the properties that have non default values
         StringBuilder result = new StringBuilder();
         var props = this.GetType().GetTypeInfo().DeclaredProperties;
         result.AppendLine("{");
         foreach (var prop in props)
         {
            if (prop.Name != "clientExtensions")
            {
               object value = prop.GetValue(this);
               bool valueIsNull = value == null;
               object defaultValue = OANDACommon.GetDefault(prop.PropertyType);
               bool defaultValueIsNull = defaultValue == null;
               if ((valueIsNull != defaultValueIsNull) // one is null when the other isn't
                   || (!valueIsNull && (value.ToString() != defaultValue.ToString()))) // both aren't null, so compare as strings
               {
                  result.AppendLine(prop.Name + " : " + prop.GetValue(this));
               }
            }
         }
         result.AppendLine("}");
         return result.ToString();
      }
   }
}
