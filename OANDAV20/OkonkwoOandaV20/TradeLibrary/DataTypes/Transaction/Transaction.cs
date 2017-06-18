namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class Transaction : ITransaction
   {
      public long id { get; set; }
      public string type { get; set; }
      public string time { get; set; }
      public int userID { get; set; }
      public string accountID { get; set; }
      public long batchID { get; set; }
      public string requestID { get; set; }
   }

   public interface ITransaction
   {
      long id { get; set; }
      string type { get; set; }
      string time { get; set; }
      int userID { get; set; }
      string accountID { get; set; }
      long batchID { get; set; }
      string requestID { get; set; }
   }
}
