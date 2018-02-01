using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   public class TransactionPagesResponse : Response
   {
      public string from { get; set; }
      public string to { get; set; }
      public int pageSize { get; set; }
      public List<string> type { get; set; }
      public int count { get; set; }
      public List<string> pages { get; set; }
   }
}
