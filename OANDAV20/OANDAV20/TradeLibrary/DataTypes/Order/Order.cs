using OANDAV20.TradeLibrary.DataTypes.Communications;
using OANDAV20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Order
{
   public class Order : Response
   {
      public long id { get; set; }
      public string createTime { get; set; }
      public string state { get; set; }
      public ClientExtensions clientExtensions { get; set; }
   }
}
