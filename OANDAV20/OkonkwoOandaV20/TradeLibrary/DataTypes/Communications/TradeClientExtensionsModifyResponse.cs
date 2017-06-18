using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradeClientExtensionsModifyResponse : Response
   {
      public TradeClientExtensionsModifyTransaction tradeClientExtensionsModifyTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
