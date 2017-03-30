using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   public class PositionsResponse : Response
   {
      public List<Position.Position> positions;
      public long lastTransactionID;
   }
}
