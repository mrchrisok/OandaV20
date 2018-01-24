namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class TransferFundsRejectTransaction : Transaction
   {
      public decimal amount { get; set; }
      public string fundingReason { get; set; }
      public string rejectReason { get; set; }
   }
}