using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   public class TransactionsResponse : Response
   {
      public List<Transaction.Transaction> transactions;
      public long lastTransactionID;
   }
}
