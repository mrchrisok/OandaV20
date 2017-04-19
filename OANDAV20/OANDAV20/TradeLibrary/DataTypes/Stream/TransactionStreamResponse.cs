using Newtonsoft.Json;
using OANDAV20.Framework.JsonConverters;
using OANDAV20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Stream
{
   /// <summary>
   /// Events are authorized transactions posted to the subject account.
   /// For more information, visit: http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   [JsonConverter(typeof(TransactionStreamResponseConverter))]
   public class TransactionStreamResponse : IHeartbeat
   {
      public TransactionHeartbeat heartbeat { get; set; }
      public ITransaction transaction { get; set; }

      public bool IsHeartbeat()
      {
         return (heartbeat != null);
      }
   }
}
