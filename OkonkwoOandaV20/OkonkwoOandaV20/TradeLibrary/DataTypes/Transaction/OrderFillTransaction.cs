using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
    public class OrderFillTransaction : Transaction
    {
        public long orderID { get; set; }
        public string clientOrderID { get; set; }
        public string instrument { get; set; }
        public long units { get; set; }

        /// <summary>
        /// This field is now deprecated and should no longer be used. The individual
        /// tradesClosed, tradeReduced and tradeOpened fields contain the
        /// exact/official price each unit was filled at.
        /// </summary>
        [Obsolete("Deprecated: Will be removed in a future API update.", false)]
        public decimal price { get; set; }

        public string reason { get; set; }
        public decimal pl { get; set; }
        public decimal financing { get; set; }
        public decimal commission { get; set; }
        public decimal accountBalance { get; set; }

        /// <summary>
        /// The Trade that was opened when the Order was filled (only provided if filling the Order resulted in a new Trade).
        /// </summary>
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
