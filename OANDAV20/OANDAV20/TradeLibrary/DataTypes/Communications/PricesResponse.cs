using OANDAV20.REST20.TradeLibrary.DataTypes.Pricing;
using System.Collections.Generic;

namespace OANDAV20.REST20.TradeLibrary.DataTypes.Communications
{
   public class PricesResponse : Response
   {
      public List<Price> prices;
   }
}
