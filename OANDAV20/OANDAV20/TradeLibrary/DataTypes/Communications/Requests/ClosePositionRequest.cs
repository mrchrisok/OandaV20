using OANDAV20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests
{
   public class ClosePositionRequest
   {
      public string longUnits { get; set; }
      public ClientExtensions longClientExtensions { get; set; }
      public string shortUnits { get; set; }
      public ClientExtensions shortClientExtensions { get; set; }
   }
}
