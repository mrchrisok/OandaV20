using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class OrderClientExtensionsModifyErrorResponse : ErrorResponse
   {
      public OrderClientExtensionsModifyRejectTransaction orderClientExtensionsModifyRejectTransaction { get; set; }
   }
}
