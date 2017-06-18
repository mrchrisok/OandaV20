using OkonkwoOandaV20.TradeLibrary.DataTypes.Trade;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Account
{
   public class Account : AccountSummary
   {
      public List<TradeSummary> trades { get; set; }
      public List<Position.Position> positions { get; set; }
      public List<Order.Order> orders { get; set; }
   }
}
