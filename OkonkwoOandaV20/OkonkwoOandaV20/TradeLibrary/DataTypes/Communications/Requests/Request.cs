using System.Reflection;
using System.Text;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests
{
   public abstract class Request
   {
      public string GetRequestString()
      {
         var result = new StringBuilder();
         bool firstJoin = true;
         foreach (var declaredField in this.GetType().GetTypeInfo().DeclaredFields)
         {
            var value = declaredField.GetValue(this);

            //var type = value.GetType();
            //if (type.IsGenericParameter && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            //{

            //}
 
            if (value != null)
            {
               if (firstJoin)
               {
                  result.Append("?");
                  firstJoin = false;
               }
               else
               {
                  result.Append("&");
               }

               result.Append(declaredField.Name + "=" + value);
            }
         }
         return result.ToString();
      }
   }
}
