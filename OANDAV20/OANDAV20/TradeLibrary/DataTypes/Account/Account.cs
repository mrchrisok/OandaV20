using OANDAV20.TradeLibrary.DataTypes.Communications.Trade;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Account
{
   public class Account : AccountSummary
   {
      public List<TradeSummary> trades { get; set; }
      public List<Position.Position> positions { get; set; }
      public List<Order.Order> orders { get; set; }
   }
}
