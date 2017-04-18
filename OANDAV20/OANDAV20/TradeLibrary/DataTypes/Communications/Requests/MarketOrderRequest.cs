using OANDAV20.TradeLibrary.DataTypes.Order;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests
{
   public class MarketOrderRequest : OrderRequest
   {
      public MarketOrderRequest()
      {
         type = OrderType.Market;
         timeInForce = TimeInForce.FillOrKill;
      }
   }
}
