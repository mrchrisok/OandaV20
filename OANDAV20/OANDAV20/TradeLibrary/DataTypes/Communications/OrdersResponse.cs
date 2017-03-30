using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   public class OrdersResponse
   {
      public List<Order.Order> orders;
      public long lastTransactionID;
   }
}
