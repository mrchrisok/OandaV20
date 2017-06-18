namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class TrailingStopLossOrderTransaction : Transaction
   {
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public double distance { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
      public string reason { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      public long? orderFillTransactionID { get; set; }
      public long? replacesOrderID { get; set; }
      public long? cancellingTransactionID { get; set; }
   }
}
 