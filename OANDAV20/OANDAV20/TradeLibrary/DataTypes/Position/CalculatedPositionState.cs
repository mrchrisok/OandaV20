namespace OANDAV20.REST20.TradeLibrary.DataTypes.Position
{
   public class CalculatedPositionState
   {
      public string instrument { get; set; }
      public double netUnrealizedPL { get; set; }
      public double longUnrealizedPL { get; set; }
      public double shortUnrealizedPL { get; set; }
   }
}