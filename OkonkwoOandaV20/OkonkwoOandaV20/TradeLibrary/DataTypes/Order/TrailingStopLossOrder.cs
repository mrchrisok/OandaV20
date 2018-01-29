namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class TrailingStopLossOrder : ExitOrder
   {
      public decimal distance { get; set; }
      public decimal trailingStopValue { get; set; }
   }
}
