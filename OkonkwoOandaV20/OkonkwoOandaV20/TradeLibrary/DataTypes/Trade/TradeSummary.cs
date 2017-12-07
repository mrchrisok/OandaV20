namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Trade
{
   public class TradeSummary : TradeBase
   {
      public long takeProfitOrderID { get; set; }
      public long stopLossOrderID { get; set; }
      public long trailingStopLossOrderID { get; set; }
   }
}
