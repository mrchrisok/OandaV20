using OANDAV20.REST20.TradeLibrary.DataTypes.Transaction;
using System.Runtime.Serialization;

namespace OANDAV20.REST20.TradeLibrary.DataTypes.Communications.Requests
{
   [DataContract]
   public class ReplaceExitOrdersRequest
   {
      [DataMember(EmitDefaultValue = false)]
      public TakeProfitDetails takeProfit { get; set; }

      [DataMember(EmitDefaultValue = false)]
      public StopLossDetails stopLoss { get; set; }

      [DataMember(EmitDefaultValue = false)]
      public TrailingStopLossDetails trailingStopLoss { get; set; }
   }
}
