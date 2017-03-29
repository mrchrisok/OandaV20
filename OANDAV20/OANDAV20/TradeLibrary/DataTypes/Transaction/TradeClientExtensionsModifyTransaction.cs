namespace OANDAV20.REST20.TradeLibrary.DataTypes.Transaction
{
   public class TradeClientExtensionsModifyTransaction : Transaction
   {
      public string type { get; set; }
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public ClientExtensions tradeClientExtensionsModify { get; set; }
   }
}
