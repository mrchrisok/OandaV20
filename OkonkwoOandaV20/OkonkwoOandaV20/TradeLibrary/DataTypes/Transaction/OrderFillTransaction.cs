using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class OrderFillTransaction : Transaction
   {
      public long orderID { get; set; }
      public string clientOrderID { get; set; }
      public string instrument { get; set; }
      public decimal units { get; set; }
      public decimal price { get; set; }
      public string reason { get; set; }
      public decimal pl { get; set; }
      public decimal financing { get; set; }
      public decimal accountBalance { get; set; }
      public TradeOpen tradeOpened { get; set; }
      public List<TradeReduce> tradesClosed { get; set; }
      public TradeReduce tradeReduced { get; set; }
   }
}
