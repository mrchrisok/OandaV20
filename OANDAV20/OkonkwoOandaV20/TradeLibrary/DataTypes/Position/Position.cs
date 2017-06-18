namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Position
{
   public class Position
   {
      public string instrument { get; set; }
      public double pl { get; set; }
      public double unrealizedPL { get; set; }
      public double resettablePL { get; set; }
      public PositionSide @long { get; set; }
      public PositionSide @short { get; set; }
   }
}
