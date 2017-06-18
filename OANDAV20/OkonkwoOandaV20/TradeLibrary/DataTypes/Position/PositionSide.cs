using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Position
{
   public class PositionSide
   {
      public double units { get; set; }
      public double averagePrice { get; set; }
      public List<long> tradeIDs { get; set; }
      public double pl { get; set; }
      public double unrealizedPL { get; set; }
      public double resettablePL { get; set; }
   }
}