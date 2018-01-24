using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class TakeProfitOrderRequest : ExitOrderRequest
   {
      public TakeProfitOrderRequest(Instrument.Instrument oandaInstrument)
         : base(oandaInstrument)
      {
         type = OrderType.TakeProfit;
      }
   }
}
