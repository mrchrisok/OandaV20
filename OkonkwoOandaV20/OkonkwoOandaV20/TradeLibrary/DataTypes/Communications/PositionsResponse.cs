using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class PositionsResponse : Response
   {
      public List<Position.Position> positions;
      public long lastTransactionID;
   }
}
