using OANDAV20.TradeLibrary.DataTypes.Communications.Instrument;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications
{
   public class CandlesResponse
   {
      public string instrument;
      public string granularity;
      public List<Candlestick> candles;
   }
}
