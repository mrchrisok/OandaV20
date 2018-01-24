namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class TransferFundsTransaction : Transaction
   {
      public decimal amount { get; set; }
      public string fundingReason { get; set; }
      public decimal accountBalance { get; set; }
   }
}