using OANDAV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class OrderClientExtensionsModifyResponse : Response
   {
      public OrderClientExtensionsModifyTransaction orderClientExtensionsModifyTransaction;
      public long lastTransactionID;
      public List<long> relatedTransactionIDs;
   }
}
