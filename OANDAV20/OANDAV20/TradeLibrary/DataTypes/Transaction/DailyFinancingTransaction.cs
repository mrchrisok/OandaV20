using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Transaction
{
   public class DailyFinancingTransaction : Transaction
   {
      public string type { get; set; }
      public double financing { get; set; }
      public double accountBalance { get; set; }
      public string accountFinancingMode { get; set; }
      public PositionFinancing positionFinancing { get; set; }
   }
}
