using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradesResponse
   {
      public List<Trade.Trade> trades;
      public string lastTransactionID;
   }
}
