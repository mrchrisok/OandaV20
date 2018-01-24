using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class TrailingStopLossOrderRequest : ExitOrderRequest
   {
      public TrailingStopLossOrderRequest(Instrument.Instrument oandaInstrument)
         : base(oandaInstrument)
      {
         type = OrderType.TrailingStopLoss;
      }

      public decimal distance { get; set; }
   }
}
