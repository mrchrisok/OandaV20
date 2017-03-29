namespace OANDAV20.REST20.TradeLibrary.DataTypes.Transaction
{
   public class TransferFundsRejectTransaction : Transaction
   {
      public string type { get; set; }
      public double amount { get; set; }
      public string fundingReason { get; set; }
      public string rejectReason { get; set; }
   }
}