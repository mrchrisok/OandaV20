using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class OrderFillTransaction : Transaction
   {
      public long orderID { get; set; }
      public string clientOrderID { get; set; }
      public string instrument { get; set; }
      public long units { get; set; }
      public decimal price { get; set; }
      public string reason { get; set; }
      public decimal pl { get; set; }
      public decimal financing { get; set; }
      public decimal commission { get; set; }
      public decimal accountBalance { get; set; }
      public TradeOpen tradeOpened { get; set; }

      /// <summary>
      /// The Trades that were closed when the Order was filled (only provided if filling the Order resulted in a closing open Trades).
      /// </summary>
      public List<TradeReduce> tradesClosed { get; set; }

      /// <summary>
      /// The Trade that was reduced when the Order was filled (only provided if filling the Order resulted in reducing an open Trade).
      /// </summary>
      public TradeReduce tradeReduced { get; set; }
   }
}
