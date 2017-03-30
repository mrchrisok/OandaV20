namespace OANDAV20.TradeLibrary.DataTypes.Communications.Transaction
{
   public class TransferFundsTransaction : Transaction
   {
      public string type { get; set; }
      public double amount { get; set; }
      public string fundingReason { get; set; }
      public double accountBalance { get; set; }
   }
}