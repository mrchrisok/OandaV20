using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
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

   public class OpenTradeFinancing
   {
      public long tradeID { get; set; }
      public double financing { get; set; }
   }

   /// <summary>
   /// For additional details, visit http://developer.oanda.com/rest-live-v20/transaction-df/#OrderCancelReason
   /// </summary>
   public class OrderCancelReason
   {
      public const string InternalServerError = "INTERNAL_SERVER_ERROR";
      public const string AccountLocked = "ACCOUNT_LOCKED";
      public const string AccountNewPositionsLocked = "ACCOUNT_NEW_POSITIONS_LOCKED";
      public const string AccountNewOrderCreationLocked = "ACCOUNT_ORDER_CREATION_LOCKED";
      public const string AccountOrderFillLocked = "ACCOUNT_ORDER_FILL_LOCKED";
      public const string ClientRequest = "CLIENT_REQUEST";
      public const string Migration = "MIGRATION";
      public const string MarketHalted = "MARKET_HALTED";
      public const string LinkedTradeClosed = "LINKED_TRADE_CLOSED";
      public const string TimeInForceExpired = "TIME_IN_FORCE_EXPIRED";
      public const string InsufficientMargin = "INSUFFICIENT_MARGIN";
      public const string FifoViolation = "FIFO_VIOLATION";
      public const string BoundsViolation = "BOUNDS_VIOLATION";
      public const string ClientRequestReplaced = "CLIENT_REQUEST_REPLACED";
      public const string InsufficientLiquidity = "INSUFFICIENT_LIQUIDITY";
      public const string TakeProfitOnFillGtdTimestampInPast = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string TakeProfitOnFillLoss = "TAKE_PROFIT_ON_FILL_LOSS";
      public const string LosingTakeProfit = "LOSING_TAKE_PROFIT";
      public const string StopLossOnFillGtdTimestampInPast = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string StopLossOnFillLoss = "STOP_LOSS_ON_FILL_LOSS";
      public const string TrailingStopLossOnFillGtdTimestampInPast = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string ClientTradeIdAlreadyExists = "CLIENT_TRADE_ID_ALREADY_EXISTS";
      public const string PositionCloseoutFailed = "POSITION_CLOSEOUT_FAILED";
      public const string OpenTradesAllowedExceeded = "OPEN_TRADES_ALLOWED_EXCEEDED";
      public const string PendingOrdersAllowedExceeded = "PENDING_ORDERS_ALLOWED_EXCEEDED";
      public const string TakeProfitOnFillClientOrderIdAlreadyExists = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string StopLossOnFillClientOrderIdAlreadyExists = "STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string TrailingStopLossOnFillClientOrderIdAlreadyExists = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string PositionSizeExceeded = "POSITION_SIZE_EXCEEDED";
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

   [JsonConverter(typeof(PriceObjectConverter))]
   public class StopLossDetails : IHasPrices
   {
      public StopLossDetails() { }
      public StopLossDetails(Instrument.Instrument instrument)
      {
         priceInformation = new PriceInformation()
         {
            instrument = instrument,
            priceProperties = new List<string>() { "price" }
         };
      }

      public double price { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      [JsonIgnore]
      public PriceInformation priceInformation { get; set; }
   }

   [JsonConverter(typeof(PriceObjectConverter))]
   public class TakeProfitDetails : IHasPrices
   {
      public TakeProfitDetails() { }
      public TakeProfitDetails(Instrument.Instrument instrument)
      {
         priceInformation = new PriceInformation()
         {
            instrument = instrument,
            priceProperties = new List<string>() { "price" }
         };
      }

      public double price { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      [JsonIgnore]
      public PriceInformation priceInformation { get; set; }
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

   [JsonConverter(typeof(PriceObjectConverter))]
   public class TrailingStopLossDetails : IHasPrices
   {
      public TrailingStopLossDetails() { }
      public TrailingStopLossDetails(Instrument.Instrument instrument)
      {
         priceInformation = new PriceInformation()
         {
            instrument = instrument,
            priceProperties = new List<string>() { "distance" }
         };
      }

      public double distance { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      [JsonIgnore]
      public PriceInformation priceInformation { get; set; }
   }

   #region order reasons
   public abstract class OrderReason
   {
      public const string ClientOrder = "CLIENT_ORDER";
   }

   public class LimitOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
   }

   public class MarketIfTouchedOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
   }

   public class MarketOrderReason : OrderReason
   {
      public const string TradeClose = "TRADE_CLOSE";
      public const string PositionCloseout = "POSITION_CLOSEOUT";
      public const string MarginCloseout = "MARGIN_CLOSEOUT";
      public const string DelayedTradeClose = "DELAYED_TRADE_CLOSE";
   }

   public class StopLossOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
      public const string OnFill = "ON_FILL";
   }

   public class StopOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
   }

   public class TakeProfitOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
      public const string OnFill = "ON_FILL";
   }

   public class TrailingStopLossOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
      public const string OnFill = "ON_FILL";
   }
   #endregion

   public class TransactionRejectReason
   {
      public const string InternalServerError = "INTERNAL_SERVER_ERROR";
      public const string InstrumentPriceUnknown = "INTERNAL_SERVER_ERROR";
      public const string AccountNotActive = "INTERNAL_SERVER_ERROR";
      public const string AccountLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountOrderCreationLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountConfigurationLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountDepositLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountWithdrawalLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountOrderCancelLocked = "INTERNAL_SERVER_ERROR";
      public const string InstrumentNotTradeable = "INTERNAL_SERVER_ERROR";
      public const string PendingOrdersAllowedExceeded = "INTERNAL_SERVER_ERROR";
      public const string OrderIdUnspecified = "ORDER_ID_UNSPECIFIED";
      public const string OrderDoesntExist = "ORDER_DOESNT_EXIST";
      public const string OrderIdentifierInconsistency = "ORDER_IDENTIFIER_INCONSISTENCY";
      public const string TradeIdUnspecified = "TRADE_ID_UNSPECIFIED";
      public const string TradeDoesntExist = "TRADE_DOESNT_EXIST";
      public const string TradeIdentifierInconsistency = "TRADE_IDENTIFIER_INCONSISTENCY";
      public const string InstrumentMissing = "INSTRUMENT_MISSING";
      public const string InstrumentUnknown = "INSTRUMENT_UNKNOWN";
      public const string UnitsMissing = "UNITS_MISSING";
      public const string UnitsInvalid = "UNITS_INVALID";
      public const string UnitsPrecisionExceeded = "UNITS_PRECISION_EXCEEDED";
      public const string UnitsLimitExceeded = "UNITS_LIMIT_EXCEEDED";
      public const string UnitsMinimumNotMet = "UNITS_MIMIMUM_NOT_MET";
      public const string PriceMissing = "PRICE_MISSING";
      public const string PriceInvalid = "PRICE_INVALID";
      public const string PricePrecisionExceeded = "PRICE_PRECISION_EXCEEDED";
      public const string PriceDistanceMissing = "PRICE_DISTANCE_MISSING";
      public const string PriceDistanceInvalid = "PRICE_DISTANCE_INVALID";
      public const string PriceDistancePrecisionExceeded = "PRICE_DISTANCE_PRECISION_EXCEEDED";
      public const string PriceDistanceMaximumExceeded = "PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      public const string PriceDistanceMinimumNotMet = "PRICE_DISTANCE_MINIMUM_NOT_MET";
      public const string TimeInForceMissing = "TIME_IN_FORCE_MISSING";
      public const string TimeInForceInvalid = "TIME_IN_FORCE_INVALID";
      public const string TimeInForceGtdTimestampMissing = "TIME_IN_FORCE_GTD_TIMESTAMP_MISSING";
      public const string TimeInForceGtdTimestampInPast = "TIME_IN_FORCE_GTD_TIMESTAMP_IN_PAST";
      public const string PriceBoundInvalid = "PRICE_BOUND_INVALID";
      public const string PriceBoundPrecisionExceeded = "PRICE_BOUND_PRECISION_EXCEEDED";
      public const string OrdersOnFillDuplicateClientOrderIDs = "ORDERS_ON_FILL_DUPLICATE_CLIENT_ORDER_IDS";
      public const string TradeOnFillClientExtensionsNotSupported = "TRADE_ON_FILL_CLIENT_EXTENSIONS_NOT_SUPPORTED";
      public const string ClientOrderIdInvalid = "CLIENT_ORDER_ID_INVALID";
      public const string ClientOrderIdAlreadyExists = "CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string ClientOrderTagInvalid = "CLIENT_ORDER_TAG_INVALID";
      public const string ClientOrderCommentInvalid = "CLIENT_ORDER_COMMENT_INVALID";
      public const string ClientTradeIdInvalid = "CLIENT_TRADE_ID_INVALID";
      public const string ClientTradeIdAlreadyExists = "CLIENT_TRADE_ID_ALREADY_EXISTS";
      public const string ClientTradeTagInvalid = "CLIENT_TRADE_TAG_INVALID";
      public const string ClientTradeCommentInvalid = "CLIENT_TRADE_COMMENT_INVALID";
      public const string OrderFillPositionActionMissing = "ORDER_FILL_POSITION_ACTION_MISSING";
      public const string OrderFillPositionActionInvalid = "ORDER_FILL_POSITION_ACTION_INVALID";
      public const string TriggerConditionMissing = "TRIGGER_CONDITION_MISSING";
      public const string TriggerConditionInvalid = "TRIGGER_CONDITION_INVALID";
      public const string OrderPartialFillOptionMissing = "ORDER_PARTIAL_FILL_OPTION_MISSING";
      public const string OrderPartialFillOptionInvalid = "ORDER_PARTIAL_FILL_OPTION_INVALID";
      public const string InvalidReissueImmediatePartialFill = "INVALID_REISSUE_IMMEDIATE_PARTIAL_FILL";
      public const string TakeProfitOrderAlreadyExists = "TAKE_PROFIT_ORDER_ALREADY_EXISTS";
      public const string TakeProfitOnFillPriceMissing = "TAKE_PROFIT_ON_FILL_PRICE_MISSING";
      public const string TakeProfitOnFillPriceInvalid = "TAKE_PROFIT_ON_FILL_PRICE_INVALID";
      public const string TakeProfitOnFillPricePrecisionExceeded = "TAKE_PROFIT_ON_FILL_PRICE_PRECISION_EXCEEDED";
      public const string TakeProfitOnFillTimeInForceMissing = "TAKE_PROFIT_ON_FILL_TIME_IN_FORCE_MISSING";
      public const string TakeProfitOnFillTimeInForceInvalid = "TAKE_PROFIT_ON_FILL_TIME_IN_FORCE_INVALID";
      public const string TakeProfitOnFillGtdTimestampMissing = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_MISSING";
      public const string TakeProfitOnFillGtdTimestampInPast = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string TakeProfitOnFillClientOrderIdInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_ID_INVALID";
      public const string TakeProfitOnFillClientOrderTagInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      public const string TakeProfitOnFillClientOrderCommentInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      public const string TakeProfitOnFillTriggerConditionMissing = "TAKE_PROFIT_ON_FILL_TRIGGER_CONDITION_MISSING";
      public const string TakeProfitOnFillTriggerConditionInvalid = "TAKE_PROFIT_ON_FILL_TRIGGER_CONDITION_INVALID";
      public const string StopLossOrderAlreadyExists = "STOP_LOSS_ORDER_ALREADY_EXISTS";
      public const string StopLossOnFillPriceMissing = "STOP_LOSS_ON_FILL_PRICE_MISSING";
      public const string StopLossOnFillPriceInvalid = "STOP_LOSS_ON_FILL_PRICE_INVALID";
      public const string StopLossOnFillPricePrecisionExceeded = "STOP_LOSS_ON_FILL_PRICE_PRECISION_EXCEEDED";
      public const string StopLossOnFillTimeInForceMissing = "STOP_LOSS_ON_FILL_TIME_IN_FORCE_MISSING";
      public const string StopLossOnFillTimeInForceInvalid = "STOP_LOSS_ON_FILL_TIME_IN_FORCE_INVALID";
      public const string StopLossOnFillGtdTimestampMissing = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_MISSING";
      public const string StopLossOnFillGtdTimestampInPast = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string StopLossOnFillClientOrderIdInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_INVALID";
      public const string StopLossOnFillClientOrderTagInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      public const string StopLossOnFillClientOrderCommentInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      public const string StopLossOnFillTriggerConditionMissing = "STOP_LOSS_ON_FILL_TRIGGER_CONDITION_MISSING";
      public const string StopLossOnFillTriggerConditionInvalid = "STOP_LOSS_ON_FILL_TRIGGER_CONDITION_INVALID";
      public const string TrailingStopLossOrderAlreadyExists = "TRAILING_STOP_LOSS_ORDER_ALREADY_EXISTS";
      public const string TrailingStopLossOnFillPriceDistanceMissing = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MISSING";
      public const string TrailingStopLossOnFillPriceDistanceInvalid = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_INVALID";
      public const string TrailingStopLossOnFillPriceDistancePrecisionExceeded = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_PRECISION_EXCEEDED";
      public const string TrailingStopLossOnFillPriceDistanceMaximumExceeded = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      public const string TrailingStopLossOnFillPriceDistanceMinimumNotMet = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MINIMUM_NOT_MET";
      public const string TrailingStopLossOnFillTimeInForceMissing = "TRAILING_STOP_LOSS_ON_FILL_TIME_IN_FORCE_MISSING";
      public const string TrailingStopLossOnFillTimeInForceInvalid = "TRAILING_STOP_LOSS_ON_FILL_TIME_IN_FORCE_INVALID";
      public const string TrailingStopLossOnFillGtdTimestampMissing = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_MISSING";
      public const string TrailingStopLossOnFillGtdTimestampInPast = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string TrailingStopLossOnFillClientOrderIdInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_INVALID";
      public const string TrailingStopLossOnFillClientOrderTagInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      public const string TrailingStopLossOnFillClientOrderCommentInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      public const string TrailingStopLossOrdersNotSupported = "TRAILING_STOP_LOSS_ORDERS_NOT_SUPPORTED";
      public const string TrailingStopLossOnFillTriggerConditionMissing = "TRAILING_STOP_LOSS_ON_FILL_TRIGGER_CONDITION_MISSING";
      public const string TrailingStopLossOnFillTriggerConditionInvalid = "TRAILING_STOP_LOSS_ON_FILL_TRIGGER_CONDITION_INVALID";
      public const string CloseTradeTypeMissing = "CLOSE_TRADE_TYPE_MISSING";
      public const string CloseTradePartialUnitsMissing = "CLOSE_TRADE_PARTIAL_UNITS_MISSING";
      public const string CloseTradeUnitsExceedTradeSize = "CLOSE_TRADE_UNITS_EXCEED_TRADE_SIZE";
      public const string CloseoutPositionDoesntExist = "CLOSEOUT_POSITION_DOESNT_EXIST";
      public const string CloseoutPositionIncompleteSpecification = "CLOSEOUT_POSITION_INCOMPLETE_SPECIFICATION";
      public const string CloseoutPositionUnitsExceedPositionSize = "CLOSEOUT_POSITION_UNITS_EXCEED_POSITION_SIZE";
      public const string CloseoutPositionReject = "CLOSEOUT_POSITION_REJECT";
      public const string CloseoutPositionPartialUnitsMissing = "CLOSEOUT_POSITION_PARTIAL_UNITS_MISSING";
      public const string MarkupGroupIdInvalid = "MARKUP_GROUP_ID_INVALID";
      public const string PositionAggregationModeInvalid = "POSITION_AGGREGATION_MODE_INVALID";
      public const string AdminConfigureDataMissing = "ADMIN_CONFIGURE_DATA_MISSING";
      public const string MarginRateInvalid = "MARGIN_RATE_INVALID";
      public const string MarginRateWouldTriggerCloseout = "MARGIN_RATE_WOULD_TRIGGER_CLOSEOUT";
      public const string AliasInvalid = "ALIAS_INVALID";
      public const string ClientConfigureDataMissing = "CLIENT_CONFIGURE_DATA_MISSING";
      public const string MarginRateWouldTriggerMarginCall = "MARGIN_RATE_WOULD_TRIGGER_MARGIN_CALL";
      public const string AmountInvalid = "AMOUNT_INVALID";
      public const string InsufficientFunds = "INSUFFICIENT_FUNDS";
      public const string AmountMissing = "AMOUNT_MISSING";
      public const string FundingReasonMissing = "FUNDING_REASON_MISSING";
      public const string ClientExtensionsDataMissing = "CLIENT_EXTENSIONS_DATA_MISSING";
      public const string ReplacingOrderInvalid = "REPLACING_ORDER_INVALID";
      public const string ReplacingTradeIdInvalid = "REPLACING_TRADE_ID_INVALID";
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
