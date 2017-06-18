using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class DailyFinancingTransaction : Transaction
   {
      public double financing { get; set; }
      public double accountBalance { get; set; }
      public string accountFinancingMode { get; set; }
      public PositionFinancing positionFinancing { get; set; }
   }
}
