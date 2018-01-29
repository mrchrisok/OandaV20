using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public abstract class ExitOrder : Order
   {
      public long tradeId { get; set; }
      public string clientTradeID { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
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
