using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class MarketIfTouchedOrderRequest : EntryOrderRequest
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
