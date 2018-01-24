using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Trade
{
    public abstract class TradeBase
    {
        public long id { get; set; }
        public string instrument { get; set; }
        public decimal price { get; set; }
        public string openTime { get; set; }
        public string state { get; set; }
        public int initialUnits { get; set; }
        public int currentUnits { get; set; }
        public decimal realizedPL { get; set; }
        public decimal unrealizedPL { get; set; }
        public decimal averageClosePrice { get; set; }
        public List<long> closingTransactionIDs { get; set; }
        public decimal financing { get; set; }
        public string closeTime { get; set; }
        public ClientExtensions clientExtensions { get; set; }
    }
}
