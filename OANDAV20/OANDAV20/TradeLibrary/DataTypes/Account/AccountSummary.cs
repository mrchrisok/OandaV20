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
      public double balance { get; set; }
      public int createdByUserID { get; set; }
      public string createdTime { get; set; }
      public double pl { get; set; }
      public double resettablePL { get; set; }
      public string resettablePLTime { get; set; }
      public double? marginRate { get; set; }
      public string marginCallEnterTime { get; set; }
      public int marginCallExtensionCount { get; set; }
      public string lastMarginCallExtensionTime { get; set; }
      public int openTradeCount { get; set; }
      public int openPositionCount { get; set; }
      public int pendingOrderCount { get; set; }
      public bool hedgingEnabled { get; set; }
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
      public long lastTransactionID { get; set; }
   }
}
