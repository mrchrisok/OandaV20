namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public abstract class ExitOrderTransaction : Transaction
   {
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
      public string reason { get; set; }
   }
}
