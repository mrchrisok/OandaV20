using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradeClientExtensionsModifyResponse : Response
   {
      public TradeClientExtensionsModifyTransaction tradeClientExtensionsModifyTransaction { get; set; }
   }
}
