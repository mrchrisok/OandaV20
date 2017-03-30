using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   public class OrderPostResponse : Response
   {
      public Transaction.Transaction orderCreateTransaction { get; set; }
      public OrderFillTransaction orderFillTransaction { get; set; }
      public OrderCancelTransaction orderCancelTransaction { get; set; }
      public Transaction.Transaction orderReissueTransaction { get; set; }
      public Transaction.Transaction orderReissueRejectTransaction { get; set; }
      public List<long> transactionIDs { get; set; }
      public long lastTransactionID { get; set; }
   }
}
 