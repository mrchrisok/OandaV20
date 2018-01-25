namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class MarketOrderTransaction : EntryOrderTransaction
   {
      public decimal? priceBound { get; set; }
      public MarketOrderTradeClose tradeClose { get; set; }
      public MarketOrderPositionCloseout longPositionCloseout { get; set; }
      public MarketOrderPositionCloseout shortPositionCloseout { get; set; }
      public MarketOrderMarginCloseout marginCloseout { get; set; }
      public MarketOrderDelayedTradeClose delayedTradeClose { get; set; }
   }
}
