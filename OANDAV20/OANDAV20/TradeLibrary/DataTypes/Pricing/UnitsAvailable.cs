namespace OANDAV20.TradeLibrary.DataTypes.Communications.Pricing
{
   public class UnitsAvailable
   {
      public UnitsAvailableDetails @default { get; set; }
      public UnitsAvailableDetails reduceFirst { get; set; }
      public UnitsAvailableDetails reduceOnly { get; set; }
      public UnitsAvailableDetails openOnly { get; set; }
   }
}