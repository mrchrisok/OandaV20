namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Position
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/position-df/#CalculatedPositionState
   /// </summary>
   public class CalculatedPositionState
   {
      public string instrument { get; set; }
      public decimal netUnrealizedPL { get; set; }
      public decimal longUnrealizedPL { get; set; }
      public decimal shortUnrealizedPL { get; set; }
   }
}