namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class LimitOrderTransaction : Transaction
   {
      public string instrument { get; set; }
      public decimal units { get; set; }
      public decimal price { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string positionFill { get; set; }
      public string triggerCondition { get; set; }
      public string reason { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      public TakeProfitDetails takeProfitOnFill { get; set; }
      public StopLossDetails stopLossOnFill { get; set; }
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }
      public ClientExtensions tradeClientExtensions { get; set; }
      public long? replacesOrderID { get; set; }
      public long? cancellingTransactionID { get; set; }
   }
}
 