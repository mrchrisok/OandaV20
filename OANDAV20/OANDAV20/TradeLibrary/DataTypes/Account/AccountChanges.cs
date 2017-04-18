using Newtonsoft.Json;
using OANDAV20.Framework.JsonConverters;
using OANDAV20.TradeLibrary.DataTypes.Order;
using OANDAV20.TradeLibrary.DataTypes.Trade;
using OANDAV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Account
{
   public class AccountChanges
   {
      [JsonConverter(typeof(OrderConverter))]
      public List<IOrder> ordersCreated { get; set; }
      [JsonConverter(typeof(OrderConverter))]
      public List<IOrder> ordersCancelled { get; set; }
      [JsonConverter(typeof(OrderConverter))]
      public List<IOrder> ordersFilled { get; set; }
      [JsonConverter(typeof(OrderConverter))]
      public List<IOrder> ordersTriggered { get; set; }

      public List<TradeSummary> tradesOpened { get; set; }
      public List<TradeSummary> tradesReduced { get; set; }
      public List<TradeSummary> tradesClosed { get; set; }
      public List<Position.Position> positions { get; set; }

      [JsonConverter(typeof(TransactionConverter))]
      public List<ITransaction> transactions { get; set; }
   }
}
