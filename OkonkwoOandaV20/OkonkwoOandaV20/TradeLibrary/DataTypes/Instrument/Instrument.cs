namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument
{
   public class Instrument
   {
      public string name { get; set; }
      public string type { get; set; }
      public string displayName { get; set; }
      public int pipLocation { get; set; }
      public int displayPrecision { get; set; }
      public int tradeUnitsPrecision { get; set; }
      public long minimumTradeSize { get; set; }
      public decimal maximumTrailingStopDistance { get; set; }
      public decimal minimumTrailingStopDistance { get; set; }
      public decimal maximumPositionSize { get; set; }
      public decimal maximumOrderUnits { get; set; }
      public decimal marginRate { get; set; }
   }
}
