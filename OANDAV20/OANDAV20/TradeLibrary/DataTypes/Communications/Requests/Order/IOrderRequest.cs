using OANDAV20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public interface IOrderRequest
   {
      string type { get; set; }
      ClientExtensions clientExtensions { get; set; }
   }
}
