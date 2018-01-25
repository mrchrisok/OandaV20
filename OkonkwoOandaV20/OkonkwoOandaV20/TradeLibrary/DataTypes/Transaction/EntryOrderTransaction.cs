namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public abstract class EntryOrderTransaction : Transaction
   {
      public string instrument { get; set; }
      public decimal units { get; set; }
      public string timeInForce { get; set; }
      public string positionFill { get; set; }
      public string reason { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      public TakeProfitDetails takeProfitOnFill { get; set; }
      public StopLossDetails stopLossOnFill { get; set; }
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }
      public ClientExtensions tradeClientExtensions { get; set; }
   }
}
