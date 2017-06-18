using OkonkwoOandaV20.TradeLibrary.DataTypes.Position;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Trade;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Account
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/account-df/#AccountChangesState
   /// </summary>
   public class AccountChangesState
   {
      public double unrealizedPL { get; set; }
      public double NAV { get; set; }
      public double marginUsed { get; set; }
      public double marginAvailable { get; set; }
      public double positionValue { get; set; }
      public double marginCloseoutUnrealizedPL { get; set; }
      public double marginCloseoutNAV { get; set; }
      public double marginCloseoutMarginUsed { get; set; }
      public double marginCloseoutPercent { get; set; }
      public double marginCloseoutPositionValue { get; set; }
      public double withdrawalLimit { get; set; }
      public double marginCallMarginUsed { get; set; }
      public double marginCallPercent { get; set; }
      public List<DynamicOrderState> orders { get; set; }
      public List<CalculatedTradeState> trades { get; set; }
      public List<CalculatedPositionState> positions { get; set; }
   }
}
