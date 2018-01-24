namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Position
{
   public class Position
   {
      public string instrument { get; set; }
      public decimal pl { get; set; }
      public decimal unrealizedPL { get; set; }
      public decimal resettablePL { get; set; }
      public PositionSide @long { get; set; }
      public PositionSide @short { get; set; }
   }
}
