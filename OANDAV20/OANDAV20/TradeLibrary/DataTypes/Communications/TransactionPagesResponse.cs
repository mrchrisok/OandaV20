using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   public class TransactionPagesResponse : Response
   {
      public string from;
      public string to;
      public int pageSize;
      public List<string> type;
      public int count;
      public List<string> pages;
      public long lastTransactionID;
   }
}
