namespace OANDAV20.TradeLibrary.DataTypes.Communications.Transaction
{
   public class TransferFundsRejectTransaction : Transaction
   {
      public string type { get; set; }
      public double amount { get; set; }
      public string fundingReason { get; set; }
      public string rejectReason { get; set; }
   }
}