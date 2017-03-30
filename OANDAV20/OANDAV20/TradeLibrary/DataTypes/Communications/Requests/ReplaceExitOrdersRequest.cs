using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;
using System.Runtime.Serialization;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications.Requests
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
