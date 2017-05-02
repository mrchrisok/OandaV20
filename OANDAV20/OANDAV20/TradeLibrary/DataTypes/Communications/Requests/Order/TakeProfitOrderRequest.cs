using OANDAV20.TradeLibrary.DataTypes.Order;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class TakeProfitOrderRequest : ExitOrderRequest
   {
      public TakeProfitOrderRequest()
      {
         type = OrderType.TakeProfit;
      }

      public double price { get; set; }
   }
}
