using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class StopOrderRequest : EntryOrderRequest
   {
      public StopOrderRequest(Instrument.Instrument oandaInstrument)
         : base(oandaInstrument)
      {
         type = OrderType.Stop;
         timeInForce = TimeInForce.GoodUntilCancelled;
         triggerCondition = OrderTriggerCondition.Default;
      }

      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
   }
}
