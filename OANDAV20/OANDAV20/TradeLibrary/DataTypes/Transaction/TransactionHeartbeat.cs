namespace OANDAV20.TradeLibrary.DataTypes.Communications.Transaction
{
   public class TransactionHeartbeat
   {
      public string type { get; set; }
      public long? lastTransactionID { get; set; }
      public string time { get; set; }
   }
}
