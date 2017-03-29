using OANDAV20.REST20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.REST20.TradeLibrary.DataTypes.Stream
{
   /// <summary>
   /// Events are authorized transactions posted to the subject account.
   /// For more information, visit: http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   public class TransactionStreamResponse : IHeartbeat
   {
      public TransactionHeartbeat heartbeat { get; set; }
      public Transaction.Transaction transaction { get; set; }

      public bool IsHeartbeat()
      {
         return (heartbeat != null);
      }
   }
}
