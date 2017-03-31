using OANDAV20.TradeLibrary.DataTypes.Instrument;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class CandlesResponse
   {
      public string instrument;
      public string granularity;
      public List<Candlestick> candles;
   }
}
