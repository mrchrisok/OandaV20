using System.Collections.Generic;

namespace OANDAV20.REST20.TradeLibrary.DataTypes.Communications
{
   public class PositionsResponse : Response
   {
      public List<Position.Position> positions;
      public long lastTransactionID;
   }
}
