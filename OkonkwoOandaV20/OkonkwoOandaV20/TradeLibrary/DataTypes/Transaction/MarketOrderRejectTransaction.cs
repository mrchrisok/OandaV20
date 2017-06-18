namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class MarketOrderRejectTransaction : Transaction
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
      public string reason { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      public TakeProfitDetails takeProfitOnFill { get; set; }
      public StopLossDetails stopLossOnFill { get; set; }
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }
      public ClientExtensions tradeClientExtensions { get; set; }
      public string rejectReason { get; set; }
   }
}
