namespace OANDAV20.REST20.TradeLibrary.DataTypes.Instrument
{
   public struct Candlestick
   {
      public string time { get; set; }
      public CandleStickData bid { get; set; }
      public CandleStickData ask { get; set; }
      public CandleStickData mid { get; set; }
      public int volume { get; set; }
      public bool complete { get; set; }
   }
}
