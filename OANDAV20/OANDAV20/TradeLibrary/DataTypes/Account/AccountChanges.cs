using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Trade;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Account
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
