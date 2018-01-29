namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class TrailingStopLossOrderRejectTransaction : ExitOrderRejectTransaction
   {
      public decimal distance { get; set; }
   }
}
 