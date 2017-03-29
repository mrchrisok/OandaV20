using OANDAV20.REST20.TradeLibrary.DataTypes.Transaction;
using System.Runtime.Serialization;

namespace OANDAV20.REST20.TradeLibrary.DataTypes.Communications.Requests
{
   [DataContract]
   public class ClosePositionRequest
   {
      [DataMember(EmitDefaultValue = false)]
      public string longUnits { get; set; }

      [DataMember(EmitDefaultValue = false)]
      public ClientExtensions longClientExtensions { get; set; }

      [DataMember(EmitDefaultValue = false)]
      public string shortUnits { get; set; }

      [DataMember(EmitDefaultValue = false)]
      public ClientExtensions shortClientExtensions { get; set; }
   }
}
