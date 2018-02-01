using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradesResponse : Response
   {
      public List<Trade.Trade> trades { get; set; }
   }
}
