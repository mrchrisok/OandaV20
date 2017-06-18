namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Position
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/position-df/#CalculatedPositionState
   /// </summary>
   public class CalculatedPositionState
   {
      public string instrument { get; set; }
      public double netUnrealizedPL { get; set; }
      public double longUnrealizedPL { get; set; }
      public double shortUnrealizedPL { get; set; }
   }
}