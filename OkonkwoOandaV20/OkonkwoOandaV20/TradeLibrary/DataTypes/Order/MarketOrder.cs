using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class MarketOrder : Order
   {
      public string instrument { get; set; }
      public double units { get; set; }
      public string timeInForce { get; set; }
      public double? priceBound { get; set; }
      public string positionFill { get; set; }
      public MarketOrderTradeClose tradeClose { get; set; }
      public MarketOrderPositionCloseout longPositionCloseout { get; set; }
      public MarketOrderPositionCloseout shortPositionCloseout { get; set; }
      public MarketOrderMarginCloseout marginCloseout { get; set; }
      public MarketOrderDelayedTradeClose delayedTradeClose { get; set; }
      public TakeProfitDetails takeProfitOnFill { get; set; }
      public StopLossDetails stopLossOnFill { get; set; }
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }
      public ClientExtensions tradeClientExtensions { get; set; }
      public long? fillingTransactionID { get; set; }
      public string filledTime { get; set; }
      public long? tradeOpenedID { get; set; }
      public long? tradeReducedID { get; set; }
      public List<long> tradeClosedIDs { get; set; }
      public long? cancellingTransactionID { get; set; }
      public string cancelledTime { get; set; }
   }
}
