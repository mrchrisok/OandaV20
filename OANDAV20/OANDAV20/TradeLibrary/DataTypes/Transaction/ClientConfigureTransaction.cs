namespace OANDAV20.TradeLibrary.DataTypes.Communications.Transaction
{
   public class ClientConfigureTransaction : Transaction
   {
      public string type { get; set; }
      public string alias { get; set; }
      public double marginRate { get; set; }
   }
}