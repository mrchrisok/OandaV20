using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class OrderClientExtensionsModifyResponse : Response
   {
      public OrderClientExtensionsModifyTransaction orderClientExtensionsModifyTransaction { get; set; }
   }
}
