using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class StopOrder : Order
   {
      public string instrument { get; set; }
      public long units { get; set; }
      public double price { get; set; }
      public double? priceBound { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string positionFill { get; set; }
      public string triggerCondition { get; set; }
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
      public long? replacesOrderID { get; set; }
      public long? replacedByOrderID { get; set; }
   }
}
