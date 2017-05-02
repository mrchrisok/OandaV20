using OANDAV20.TradeLibrary.DataTypes.Order;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests.Order
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
