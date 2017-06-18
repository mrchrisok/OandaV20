namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Pricing
{
   public class UnitsAvailable
   {
      public UnitsAvailableDetails @default { get; set; }
      public UnitsAvailableDetails reduceFirst { get; set; }
      public UnitsAvailableDetails reduceOnly { get; set; }
      public UnitsAvailableDetails openOnly { get; set; }
   }
}