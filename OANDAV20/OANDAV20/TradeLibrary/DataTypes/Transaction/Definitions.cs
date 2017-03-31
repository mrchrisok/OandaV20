using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Transaction
{
   public class FundingReason
   {
      public const string ClientFunding = "CLIENT_FUNDING";
      public const string AccountTransfer = "ACCOUNT_TRANSFER";
      public const string DivisionMigration = "DIVISION_MIGRATION";
      public const string SiteMigration = "SITE_MIGRATION";
      public const string Adjustment = "ADJUSTMENT";
   }

   public class LiquidityRegenerationSchedule
   {
      public List<LiquidityRegenerationScheduleStep> steps { get; set; }
   }

   public class LiquidityRegenerationScheduleStep
   {
      public string timestamp { get; set; }
      public double bidLiquidityUsed { get; set; }
      public double askLiquidityUsed { get; set; }
   }

   public class MarketIfTouchedOrderReason
   {
      public const string ClientOrder = "CLIENT_ORDER";
      public const string Replacement = "REPLACEMENT";
   }

   public class MarketOrderDelayedTradeClose
   {
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public long sourceTransactionID { get; set; }
   }

   public class MarketOrderTradeClose
   {
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public string units { get; set; }
   }

   public class MarketOrderPositionCloseout
   {
      public string instrument { get; set; }
      public string units { get; set; }
   }

   public class MarketOrderMarginCloseout
   {
      public string reason { get; set; }
   }

   public class MarketOrderMarginCloseoutReason
   {
      public const string MarginCheckViolation = "MARGIN_CHECK_VIOLATION";
      public const string RegulatoryMarginCallViolation = "REGULATORY_MARGIN_CALL_VIOLATION";
   }

   public class MarketOrderReason
   {
      public const string ClientOrder = "CLIENT_ORDER";
      public const string TradeClose = "TRADE_CLOSE";
      public const string PositionCloseout = "POSITION_CLOSEOUT";
      public const string MarginCloseout = "MARGIN_CLOSEOUT";
      public const string DelayedTradeClose = "DELAYED_TRADE_CLOSE";
   }

   public class OpenTradeFinancing
   {
      public long tradeID { get; set; }
      public double financing { get; set; }
   }

   public class OrderFillReason
   {
      public const string LimitOrder = "LIMIT_ORDER";
      public const string StopOrder = "STOP_ORDER";
      public const string MarketIfTouchedOrder = "MARKET_IF_TOUCHED_ORDER";
      public const string TakeProfitOrder = "TAKE_PROFIT_ORDER";
      public const string StopLossOrder = "STOP_LOSS_ORDER";
      public const string TrailingStopLossOrder = "TRALING_STOP_LOSS_ORDER";
      public const string MarketOrder = "MARKET_ORDER";
      public const string MarketOrderTradeClose = "MARKET_ORDER_TRADE_CLOSE";
      public const string MarketOrderPositionCloseout = "MARKET_ORDER_POSITION_CLOSEOUT";
      public const string MarketOrderMarginCloseout = "MARKET_ORDER_MARGIN_CLOSEOUT";
      public const string MarketOrderDelayedClose = "MARKET_ORDER_DELAYED_TRADE_CLOSE";
   }

   public class PositionFinancing
   {
      public string instrumentID { get; set; }
      public double financing { get; set; }
      public List<OpenTradeFinancing> openTradeFinancings { get; set; }
   }

   public class StopLossDetails
   {
      public long price { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public ClientExtensions clientExtensiosn { get; set; }
   }

   public class TakeProfitDetails
   {
      public long price { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public ClientExtensions clientExtensiosn { get; set; }
   }

   public class TradeOpen
   {
      public long tradeID { get; set; }
      public double units { get; set; }
      public ClientExtensions clientExtensions { get; set; }
   }

   public class TradeReduce
   {
      public long tradeID { get; set; }
      public double units { get; set; }
      public double realizedPL { get; set; }
      public double financing { get; set; }
   }

   public class TrailingStopLossDetails
   {
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public ClientExtensions clientExtensiosn { get; set; }
   }

   public class TransactionRejectReason
   {
      string InternalServerError = "INTERNAL_SERVER_ERROR";
      string InstrumentPriceUnknown = "INTERNAL_SERVER_ERROR";
      string AccountNotActive = "INTERNAL_SERVER_ERROR";
      string AccountLocked = "INTERNAL_SERVER_ERROR";
      string AccountOrderCreationLocked = "INTERNAL_SERVER_ERROR";
      string AccountConfigurationLocked = "INTERNAL_SERVER_ERROR";
      string AccountDepositLocked = "INTERNAL_SERVER_ERROR";
      string AccountWithdrawalLocked = "INTERNAL_SERVER_ERROR";
      string AccountOrderCancelLocked = "INTERNAL_SERVER_ERROR";
      string InstrumentNotTradeable = "INTERNAL_SERVER_ERROR";
      string PendingOrdersAllowedExceeded = "INTERNAL_SERVER_ERROR";
      string OrderIdUnspecified = "ORDER_ID_UNSPECIFIED";
      string OrderDoesntExist = "ORDER_DOESNT_EXIST";
      string OrderIdentifierInconsistency = "ORDER_IDENTIFIER_INCONSISTENCY";
      string TradeIdUnspecified = "TRADE_ID_UNSPECIFIED";
      string TradeDoesntExist = "TRADE_DOESNT_EXIST";
      string TradeIdentifierInconsistency = "TRADE_IDENTIFIER_INCONSISTENCY";
      string InstrumentMissing = "INSTRUMENT_MISSING";
      string InstrumentUnknown = "INSTRUMENT_UNKNOWN";
      string UnitsMissing = "UNITS_MISSING";
      string UnitsInvalid = "UNITS_INVALID";
      string UnitsPrecisionExceeded = "UNITS_PRECISION_EXCEEDED";
      string UnitsLimitExceeded = "UNITS_LIMIT_EXCEEDED";
      string UnitsMinimumNotMet = "UNITS_MIMIMUM_NOT_MET";
      string PriceMissing = "PRICE_MISSING";
      string PriceInvalid = "PRICE_INVALID";
      string PricePrecisionExceeded = "PRICE_PRECISION_EXCEEDED";
      string PriceDistanceMissing = "PRICE_DISTANCE_MISSING";
      string PriceDistanceInvalid = "PRICE_DISTANCE_INVALID";
      string PriceDistancePrecisionExceeded = "PRICE_DISTANCE_PRECISION_EXCEEDED";
      string PriceDistanceMaximumExceeded = "PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      string PriceDistanceMinimumNotMet = "PRICE_DISTANCE_MINIMUM_NOT_MET";
      string TimeInForceMissing = "TIME_IN_FORCE_MISSING";
      string TimeInForceInvalid = "TIME_IN_FORCE_INVALID";
      string TimeInForceGtdTimestampMissing = "TIME_IN_FORCE_GTD_TIMESTAMP_MISSING";
      string TimeInForceGtdTimestampInPast = "TIME_IN_FORCE_GTD_TIMESTAMP_IN_PAST";
      string PriceBoundInvalid = "PRICE_BOUND_INVALID";
      string PriceBoundPrecisionExceeded = "PRICE_BOUND_PRECISION_EXCEEDED";
      string OrdersOnFillDuplicateClientOrderIDs = "ORDERS_ON_FILL_DUPLICATE_CLIENT_ORDER_IDS";
      string TradeOnFillClientExtensionsNotSupported = "TRADE_ON_FILL_CLIENT_EXTENSIONS_NOT_SUPPORTED";
      string ClientOrderIdInvalid = "CLIENT_ORDER_ID_INVALID";
      string ClientOrderIdAlreadyExists = "CLIENT_ORDER_ID_ALREADY_EXISTS";
      string ClientOrderTagInvalid = "CLIENT_ORDER_TAG_INVALID";
      string ClientOrderCommentInvalid = "CLIENT_ORDER_COMMENT_INVALID";
      string ClientTradeIdInvalid = "CLIENT_TRADE_ID_INVALID";
      string ClientTradeIdAlreadyExists = "CLIENT_TRADE_ID_ALREADY_EXISTS";
      string ClientTradeTagInvalid = "CLIENT_TRADE_TAG_INVALID";
      string ClientTradeCommentInvalid = "CLIENT_TRADE_COMMENT_INVALID";
      string OrderFillPositionActionMissing = "ORDER_FILL_POSITION_ACTION_MISSING";
      string OrderFillPositionActionInvalid = "ORDER_FILL_POSITION_ACTION_INVALID";
      string TriggerConditionMissing = "TRIGGER_CONDITION_MISSING";
      string TriggerConditionInvalid = "TRIGGER_CONDITION_INVALID";
      string OrderPartialFillOptionMissing = "ORDER_PARTIAL_FILL_OPTION_MISSING";
      string OrderPartialFillOptionInvalid = "ORDER_PARTIAL_FILL_OPTION_INVALID";
      string InvalidReissueImmediatePartialFill = "INVALID_REISSUE_IMMEDIATE_PARTIAL_FILL";
      string TakeProfitOrderAlreadyExists = "TAKE_PROFIT_ORDER_ALREADY_EXISTS";
      string TakeProfitOnFillPriceMissing = "TAKE_PROFIT_ON_FILL_PRICE_MISSING";
      string TakeProfitOnFillPriceInvalid = "TAKE_PROFIT_ON_FILL_PRICE_INVALID";
      string TakeProfitOnFillPricePrecisionExceeded = "TAKE_PROFIT_ON_FILL_PRICE_PRECISION_EXCEEDED";
      string TakeProfitOnFillTimeInForceMissing = "TAKE_PROFIT_ON_FILL_TIME_IN_FORCE_MISSING";
      string TakeProfitOnFillTimeInForceInvalid = "TAKE_PROFIT_ON_FILL_TIME_IN_FORCE_INVALID";
      string TakeProfitOnFillGtdTimestampMissing = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_MISSING";
      string TakeProfitOnFillGtdTimestampInPast = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      string TakeProfitOnFillClientOrderIdInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_ID_INVALID";
      string TakeProfitOnFillClientOrderTagInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      string TakeProfitOnFillClientOrderCommentInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      string TakeProfitOnFillTriggerConditionMissing = "TAKE_PROFIT_ON_FILL_TRIGGER_CONDITION_MISSING";
      string TakeProfitOnFillTriggerConditionInvalid = "TAKE_PROFIT_ON_FILL_TRIGGER_CONDITION_INVALID";               
      string StopLossOrderAlreadyExists = "STOP_LOSS_ORDER_ALREADY_EXISTS";
      string StopLossOnFillPriceMissing = "STOP_LOSS_ON_FILL_PRICE_MISSING";
      string StopLossOnFillPriceInvalid = "STOP_LOSS_ON_FILL_PRICE_INVALID";
      string StopLossOnFillPricePrecisionExceeded = "STOP_LOSS_ON_FILL_PRICE_PRECISION_EXCEEDED";
      string StopLossOnFillTimeInForceMissing = "STOP_LOSS_ON_FILL_TIME_IN_FORCE_MISSING";
      string StopLossOnFillTimeInForceInvalid = "STOP_LOSS_ON_FILL_TIME_IN_FORCE_INVALID";
      string StopLossOnFillGtdTimestampMissing = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_MISSING";
      string StopLossOnFillGtdTimestampInPast = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      string StopLossOnFillClientOrderIdInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_INVALID";
      string StopLossOnFillClientOrderTagInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      string StopLossOnFillClientOrderCommentInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      string StopLossOnFillTriggerConditionMissing = "STOP_LOSS_ON_FILL_TRIGGER_CONDITION_MISSING";
      string StopLossOnFillTriggerConditionInvalid = "STOP_LOSS_ON_FILL_TRIGGER_CONDITION_INVALID";
      string TrailingStopLossOrderAlreadyExists = "TRAILING_STOP_LOSS_ORDER_ALREADY_EXISTS";
      string TrailingStopLossOnFillPriceDistanceMissing = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MISSING";
      string TrailingStopLossOnFillPriceDistanceInvalid = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_INVALID";
      string TrailingStopLossOnFillPriceDistancePrecisionExceeded = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_PRECISION_EXCEEDED";
      string TrailingStopLossOnFillPriceDistanceMaximumExceeded = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      string TrailingStopLossOnFillPriceDistanceMinimumNotMet = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MINIMUM_NOT_MET";
      string TrailingStopLossOnFillTimeInForceMissing = "TRAILING_STOP_LOSS_ON_FILL_TIME_IN_FORCE_MISSING";
      string TrailingStopLossOnFillTimeInForceInvalid = "TRAILING_STOP_LOSS_ON_FILL_TIME_IN_FORCE_INVALID";
      string TrailingStopLossOnFillGtdTimestampMissing = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_MISSING";
      string TrailingStopLossOnFillGtdTimestampInPast = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      string TrailingStopLossOnFillClientOrderIdInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_INVALID";
      string TrailingStopLossOnFillClientOrderTagInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      string TrailingStopLossOnFillClientOrderCommentInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      string TrailingStopLossOrdersNotSupported = "TRAILING_STOP_LOSS_ORDERS_NOT_SUPPORTED";
      string TrailingStopLossOnFillTriggerConditionMissing = "TRAILING_STOP_LOSS_ON_FILL_TRIGGER_CONDITION_MISSING";
      string TrailingStopLossOnFillTriggerConditionInvalid = "TRAILING_STOP_LOSS_ON_FILL_TRIGGER_CONDITION_INVALID";
      string CloseTradeTypeMissing = "CLOSE_TRADE_TYPE_MISSING";
      string CloseTradePartialUnitsMissing = "CLOSE_TRADE_PARTIAL_UNITS_MISSING";
      string CloseTradeUnitsExceedTradeSize = "CLOSE_TRADE_UNITS_EXCEED_TRADE_SIZE";
      string CloseoutPositionDoesntExist = "CLOSEOUT_POSITION_DOESNT_EXIST";
      string CloseoutPositionIncompleteSpecification = "CLOSEOUT_POSITION_INCOMPLETE_SPECIFICATION";
      string CloseoutPositionUnitsExceedPositionSize = "CLOSEOUT_POSITION_UNITS_EXCEED_POSITION_SIZE";
      string CloseoutPositionReject = "CLOSEOUT_POSITION_REJECT";
      string CloseoutPositionPartialUnitsMissing = "CLOSEOUT_POSITION_PARTIAL_UNITS_MISSING";
      string MarkupGroupIdInvalid = "MARKUP_GROUP_ID_INVALID";
      string PositionAggregationModeInvalid = "POSITION_AGGREGATION_MODE_INVALID";
      string AdminConfigureDataMissing = "ADMIN_CONFIGURE_DATA_MISSING";
      string MarginRateInvalid  = "MARGIN_RATE_INVALID";
      string MarginRateWouldTriggerCloseout = "MARGIN_RATE_WOULD_TRIGGER_CLOSEOUT";
      string AliasInvalid = "ALIAS_INVALID";
      string ClientConfigureDataMissing  = "CLIENT_CONFIGURE_DATA_MISSING";
      string MarginRateWouldTriggerMarginCall = "MARGIN_RATE_WOULD_TRIGGER_MARGIN_CALL";
      string AmountInvalid = "AMOUNT_INVALID";
      string InsufficientFunds = "INSUFFICIENT_FUNDS";
      string AmountMissing = "AMOUNT_MISSING";
      string FundingReasonMissing = "FUNDING_REASON_MISSING";
      string ClientExtensionsDataMissing = "CLIENT_EXTENSIONS_DATA_MISSING";
      string ReplacingOrderInvalid = "REPLACING_ORDER_INVALID";
      string ReplacingTradeIdInvalid = "REPLACING_TRADE_ID_INVALID";
   }

   public class TransactionType
   {
      public const string Create = "CREATE";
      public const string Close = "CLOSE";
      public const string Reopen = "REOPEN";
      public const string ClientConfigure = "CLIENT_CONFIGURE";
      public const string ClientConfigureReject = "CLIENT_CONFIGURE_REJECT";
      public const string TransferFunds = "TRANSFER_FUNDS";
      public const string TransferFundsReject = "TRANSFER_FUNDS_REJECT";
      public const string MarketOrder = "MARKET_ORDER";
      public const string MarketOrderReject = "MARKET_ORDER_REJECT";
      public const string LimitOrder = "LIMIT_ORDER";
      public const string LimitOrderReject = "LIMIT_ORDER_REJECT";
      public const string StopOrder = "STOP_ORDER";
      public const string StopOrderReject = "STOP_ORDER_REJECT";
      public const string MarketIfTouchedOrder = "MARKET_IF_TOUCHED_ORDER";
      public const string MarketIfTouchedOrderReject = "MARKET_IF_TOUCHED_ORDER_REJECT";
      public const string TakeProfitOrder = "TAKE_PROFIT_ORDER";
      public const string TakeProfitOrderReject = "TAKE_PROFIT_ORDER_REJECT";
      public const string StopLossOrder = "STOP_LOSS_ORDER";
      public const string StopLossOrderReject = "STOP_LOSS_ORDER_REJECT";
      public const string TrailingStopLossOrder = "TRAILING_STOP_LOSS_ORDER";
      public const string TrailingStopLossOrderReject = "TRAILING_STOP_LOSS_ORDER_REJECT";
      public const string OrderFill = "ORDER_FILL";
      public const string OrderCancel = "ORDER_CANCEL";
      public const string OrderCancelReject = "ORDER_CANCEL_REJECT";
      public const string OrderClientExtensionsModify = "ORDER_CLIENT_EXTENSIONS_MODIFY";
      public const string OrderClientExtensionsModifyReject = "ORDER_CLIENT_EXTENSIONS_MODIFY_REJECT";
      public const string TradeClientExtensionsModify = "TRADE_CLIENT_EXTENSIONS_MODIFY";
      public const string TradeClientExtensionsModifyReject = "TRADE_CLIENT_EXTENSIONS_MODIFY_REJECT";
      public const string MarginCallEnter = "MARGIN_CALL_ENTER";
      public const string MarginCallExtend = "MARGIN_CALL_EXTEND";
      public const string MarginCallExit = "MARGIN_CALL_EXIT";
      public const string DailyFinancing = "DAILY_FINANCING";
      public const string DelayedTradeClosure = "DELAYED_TRADE_CLOSURE";
      public const string ResetResettablePL = "RESET_RESETTABLE_PL";
   }

   public class VWAPReceipt
   {
      public double units { get; set; }
      public double price { get; set; }
   }
}
