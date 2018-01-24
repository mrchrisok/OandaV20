using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class StopLossOrderRequest : ExitOrderRequest
   {
      public StopLossOrderRequest(Instrument.Instrument oandaInstrument)
         : base(oandaInstrument)
      {
         type = OrderType.StopLoss;
      }
   }
}
