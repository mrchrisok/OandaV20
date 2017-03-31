using OANDAV20.TradeLibrary.DataTypes.Trade;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Account
{
   public class AccountChanges
   {
      public List<Order.Order> ordersCreated { get; set; }
      public List<Order.Order> ordersCancelled { get; set; }
      public List<Order.Order> ordersFilled { get; set; }
      public List<Order.Order> ordersTriggered { get; set; }
      public List<TradeSummary> tradesOpened { get; set; }
      public List<TradeSummary> tradesReduced { get; set; }
      public List<TradeSummary> tradesClosed { get; set; }
      public List<Position.Position> positions { get; set; }
      public List<Transaction.Transaction> transactions { get; set; }
   }
}
