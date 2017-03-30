using OANDAV20.TradeLibrary.DataTypes.Communications.Pricing;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   public class PricesResponse : Response
   {
      public List<Price> prices;
   }
}
