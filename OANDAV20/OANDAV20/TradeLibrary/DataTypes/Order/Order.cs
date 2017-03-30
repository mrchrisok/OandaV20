using OANDAV20.TradeLibrary.DataTypes.Communications.Communications;
using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Order
{
   public class Order : Response
   {
      public long id { get; set; }
      public string createTime { get; set; }
      public string state { get; set; }
      public ClientExtensions clientExtensions { get; set; }
   }
}
