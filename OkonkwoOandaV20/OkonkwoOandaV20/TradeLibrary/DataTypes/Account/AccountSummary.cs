namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Account
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/account-df/#AccountSummary
   /// </summary>
   public class AccountSummary
   {
      public string id { get; set; }
      public string alias { get; set; }
      public string currency { get; set; }
      public decimal balance { get; set; }
      public int createdByUserID { get; set; }
      public string createdTime { get; set; }
      public decimal pl { get; set; }
      public decimal resettablePL { get; set; }
      public string resettablePLTime { get; set; }
      public decimal? marginRate { get; set; }
      public string marginCallEnterTime { get; set; }
      public int marginCallExtensionCount { get; set; }
      public string lastMarginCallExtensionTime { get; set; }
      public int openTradeCount { get; set; }
      public int openPositionCount { get; set; }
      public int pendingOrderCount { get; set; }
      public bool hedgingEnabled { get; set; }
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
      public long lastTransactionID { get; set; }
   }
}
