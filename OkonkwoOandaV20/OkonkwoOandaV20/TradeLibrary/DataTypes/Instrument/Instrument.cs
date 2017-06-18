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
      public double minimumTradeSize { get; set; }
      public double maximumTrailingStopDistance { get; set; }
      public double minimumTrailingStopDistance { get; set; }
      public double maximumPositionSize { get; set; }
      public double maximumOrderUnits { get; set; }
      public double marginRate { get; set; }
   }
}
