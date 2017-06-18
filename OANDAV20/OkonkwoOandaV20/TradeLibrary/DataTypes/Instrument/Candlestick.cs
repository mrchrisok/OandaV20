namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument
{
   public class Candlestick
   {
      public string time { get; set; }
      public CandleStickData bid { get; set; }
      public CandleStickData ask { get; set; }
      public CandleStickData mid { get; set; }
      public int volume { get; set; }
      public bool complete { get; set; }
   }

   public class CandlestickPlus : Candlestick
   {
      public CandlestickPlus() { }
      public CandlestickPlus(Candlestick candlestick)
      {
         time = candlestick.time;
         bid = candlestick.bid;
         ask = candlestick.ask;
         mid = candlestick.mid;
         volume = candlestick.volume;
         complete = candlestick.complete;
      }
      public string instrument { get; set; }
      public string granularity { get; set; }
   }
}
