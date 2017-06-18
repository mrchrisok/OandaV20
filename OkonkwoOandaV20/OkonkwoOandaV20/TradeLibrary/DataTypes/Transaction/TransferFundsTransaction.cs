namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class TransferFundsTransaction : Transaction
   {
      public double amount { get; set; }
      public string fundingReason { get; set; }
      public double accountBalance { get; set; }
   }
}