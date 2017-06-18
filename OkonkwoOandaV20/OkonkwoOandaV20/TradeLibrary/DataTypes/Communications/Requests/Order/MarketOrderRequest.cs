using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
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
