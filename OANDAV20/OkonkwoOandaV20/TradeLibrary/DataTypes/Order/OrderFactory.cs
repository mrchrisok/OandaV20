using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class OrderFactory
   {
      public static List<IOrder> Create (IEnumerable<IOrder> data)
      {
         var orders = new List<IOrder>();

         foreach (IOrder order in data)
         {
            orders.Add(Create(order.type));
         }

         return orders;
      }

      public static IOrder Create(string type)
      {
         switch(type)
         {
            case OrderType.Limit: return new LimitOrder();
            case OrderType.Market: return new MarketOrder();
            case OrderType.MarketIfTouched: return new MarketIfTouchedOrder();
            case OrderType.Stop: return new StopOrder();
            case OrderType.StopLoss: return new StopLossOrder();
            case OrderType.TakeProfit: return new TakeProfitOrder();
            case OrderType.TrailingStopLoss: return new TrailingStopLossOrder();
            default: return new Order();
         }
      }
   }
}
