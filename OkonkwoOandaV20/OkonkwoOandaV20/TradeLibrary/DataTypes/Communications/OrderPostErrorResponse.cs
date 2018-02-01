using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class OrderPostErrorResponse : ErrorResponse
   {
      [JsonConverter(typeof(TransactionConverter))]
      public ITransaction orderRejectTransaction { get; set; }
   }
}
 