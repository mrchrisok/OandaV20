using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Position
{
   public class PositionSide
   {
      public long units { get; set; }
      public decimal averagePrice { get; set; }
      public List<long> tradeIDs { get; set; }
      public decimal pl { get; set; }
      public decimal unrealizedPL { get; set; }
      public decimal resettablePL { get; set; }
   }
}