namespace OANDAV20.REST20.TradeLibrary.DataTypes.Transaction
{
   public class Transaction
   {
      public long id { get; set; }
      public string time { get; set; }
      public int userID { get; set; }
      public string accountID { get; set; }
      public long batchID { get; set; }
      public string requestID { get; set; }
   }
}
