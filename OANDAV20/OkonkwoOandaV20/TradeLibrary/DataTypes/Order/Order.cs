using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class Order : Response, IOrder
   {
      public string type { get; set; }
      public long id { get; set; }
      public string createTime { get; set; }
      public string state { get; set; }
      public ClientExtensions clientExtensions { get; set; }
   }

   public interface IOrder
   {
      string type { get; set; }
      long id { get; set; }
      string createTime { get; set; }
      string state { get; set; }
      ClientExtensions clientExtensions { get; set; }
   }
}
