using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   public class TransactionsResponse : Response
   {
      [JsonConverter(typeof(TransactionConverter))]
      public List<ITransaction> transactions;
      public long lastTransactionID;
   }
}
