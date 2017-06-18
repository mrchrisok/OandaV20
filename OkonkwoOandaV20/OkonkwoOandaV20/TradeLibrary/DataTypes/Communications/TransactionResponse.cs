using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   public class TransactionResponse : Response
   {
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction transaction;
      public long lastTransactionID;
   }
}
