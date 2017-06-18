using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class TrailingStopLossOrderRequest : ExitOrderRequest
   {
      public TrailingStopLossOrderRequest()
      {
         type = OrderType.TrailingStopLoss;
      }

      public double distance { get; set; }
   }
}
