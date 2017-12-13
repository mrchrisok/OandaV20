using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using System.Collections.Generic;

namespace OkonkwoOandaV20.Framework.Factories
{
    public class OrderFactory
    {
        public static List<IOrder> Create(IEnumerable<IOrder> data)
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
            IOrder order;

            switch (type)
            {
                case OrderType.Limit: order = new LimitOrder(); break;
                case OrderType.Market: order = new MarketOrder(); break;
                case OrderType.MarketIfTouched: order = new MarketIfTouchedOrder(); break;
                case OrderType.Stop: order = new StopOrder(); break;
                case OrderType.StopLoss: order = new StopLossOrder(); break;
                case OrderType.TakeProfit: order = new TakeProfitOrder(); break;
                case OrderType.TrailingStopLoss: order = new TrailingStopLossOrder(); break;
                default: order = new Order(); break;
            }

            order.type = type;

            return order;
        }
    }
}
