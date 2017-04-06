using OANDAV20.TradeLibrary.DataTypes.Order;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests
{
   public class MarketIfTouchedOrderRequest : OrderRequest
   {
      public MarketIfTouchedOrderRequest()
      {
         type = OrderType.MarketIfTouched;
         timeInForce = TimeInForce.GoodUntilCancelled;
         triggerCondition = OrderTriggerCondition.Default;
      }

      public double price { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
   }
}
