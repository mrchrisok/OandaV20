using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class MarketOrder : EntryOrder
   {
      public decimal? priceBound { get; set; }
      public MarketOrderTradeClose tradeClose { get; set; }
      public MarketOrderPositionCloseout longPositionCloseout { get; set; }
      public MarketOrderPositionCloseout shortPositionCloseout { get; set; }
      public MarketOrderMarginCloseout marginCloseout { get; set; }
      public MarketOrderDelayedTradeClose delayedTradeClose { get; set; }
   }
}
