using OANDAV20.TradeLibrary.DataTypes.Order;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public class MarketOrderRequest : EntryOrderRequest
   {
      public MarketOrderRequest()
      {
         type = OrderType.Market;
         timeInForce = TimeInForce.FillOrKill;
      }
   }
}
