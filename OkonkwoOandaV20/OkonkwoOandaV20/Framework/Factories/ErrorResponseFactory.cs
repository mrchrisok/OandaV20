using Newtonsoft.Json;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;

namespace OkonkwoOandaV20.Framework.Factories
{
   public class ErrorResponseFactory
   {
      public static IErrorResponse Create(string message)
      {
         var dynamicError = JsonConvert.DeserializeObject<dynamic>(message);

         // the 'type' property is injected by the Rest20.GetWebResponse catch block

         switch (dynamicError.type.ToString())
         {
            case "AccountConfigurationErrorResponse":
               return JsonConvert.DeserializeObject<AccountConfigurationErrorResponse>(message);
            case "OrderCancelErrorResponse":
               return JsonConvert.DeserializeObject<OrderCancelErrorResponse>(message);
            case "OrderCancelReplaceErrorResponse":
               return JsonConvert.DeserializeObject<OrderCancelReplaceErrorResponse>(message);
            case "OrderClientExtensionsModifyErrorResponse":
               return JsonConvert.DeserializeObject<OrderClientExtensionsModifyErrorResponse>(message);
            case "OrderPostErrorResponse":
               return JsonConvert.DeserializeObject<OrderPostErrorResponse>(message);
            case "PositionCloseErrorResponse":
               return JsonConvert.DeserializeObject<PositionCloseErrorResponse>(message);
            case "TradeClientExtensionsModifyErrorResponse":
               return JsonConvert.DeserializeObject<TradeClientExtensionsModifyErrorResponse>(message);
            case "TradeCloseErrorResponse":
               return JsonConvert.DeserializeObject<TradeCloseErrorResponse>(message);
            case "TradePatchExitOrdersErrorResponse":
               return JsonConvert.DeserializeObject<TradePatchExitOrdersErrorResponse>(message);
            default:
               return JsonConvert.DeserializeObject<ErrorResponse>(message);
         }
      }
   }
}
