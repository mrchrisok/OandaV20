namespace OANDAV20.TradeLibrary.DataTypes.Transaction
{
   public class ClientConfigureRejectTransaction : Transaction
   {
      public string type { get; set; }
      public string alias { get; set; }
      public double marginRate { get; set; }
      public string rejectReason { get; set; }
   }
}