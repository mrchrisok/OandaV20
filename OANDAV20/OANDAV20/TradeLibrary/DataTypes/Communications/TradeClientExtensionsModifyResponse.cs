using OANDAV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class TradeClientExtensionsModifyResponse : Response
   {
      public TradeClientExtensionsModifyTransaction tradeClientExtensionsModifyTransaction;
      public List<long> relatedTransactionIDs;
      public long lastTransactionID;
   }
}
