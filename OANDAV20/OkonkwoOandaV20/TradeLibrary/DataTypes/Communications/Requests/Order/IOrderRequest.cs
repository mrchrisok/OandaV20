using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public interface IOrderRequest
   {
      string type { get; set; }
      ClientExtensions clientExtensions { get; set; }
   }
}
