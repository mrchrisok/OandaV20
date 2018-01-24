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
      public decimal unrealizedPL { get; set; }
      public decimal NAV { get; set; }
      public decimal marginUsed { get; set; }
      public decimal marginAvailable { get; set; }
      public decimal positionValue { get; set; }
      public decimal marginCloseoutUnrealizedPL { get; set; }
      public decimal marginCloseoutNAV { get; set; }
      public decimal marginCloseoutMarginUsed { get; set; }
      public decimal marginCloseoutPercent { get; set; }
      public decimal marginCloseoutPositionValue { get; set; }
      public decimal withdrawalLimit { get; set; }
      public decimal marginCallMarginUsed { get; set; }
      public decimal marginCallPercent { get; set; }
      public List<DynamicOrderState> orders { get; set; }
      public List<CalculatedTradeState> trades { get; set; }
      public List<CalculatedPositionState> positions { get; set; }
   }
}
