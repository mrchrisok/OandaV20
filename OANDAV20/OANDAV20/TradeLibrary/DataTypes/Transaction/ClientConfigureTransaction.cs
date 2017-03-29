namespace OANDAV20.REST20.TradeLibrary.DataTypes.Transaction
{
   public class ClientConfigureTransaction : Transaction
   {
      public string type { get; set; }
      public string alias { get; set; }
      public double marginRate { get; set; }
   }
}