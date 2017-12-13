using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.Framework.Factories
{
   public class TransactionFactory
   {
      public static List<ITransaction> Create (IEnumerable<ITransaction> data)
      {
         var transactions = new List<ITransaction>();

         foreach (ITransaction transaction in data)
         {
            transactions.Add(Create(transaction.type));
         }

         return transactions;
      }

      public static ITransaction Create(string type)
      {
         switch(type)
         {
            case TransactionType.Create: return new CreateTransaction();
            case TransactionType.Close: return new CloseTransaction(); 
            case TransactionType.Reopen: return new ReopenTransaction();
            case TransactionType.ClientConfigure: return new ClientConfigureTransaction();
            case TransactionType.ClientConfigureReject: return new ClientConfigureRejectTransaction();
            case TransactionType.TransferFunds: return new TransferFundsTransaction();
            case TransactionType.TransferFundsReject: return new TransferFundsRejectTransaction();
            case TransactionType.MarketOrder: return new MarketOrderTransaction();
            case TransactionType.MarketOrderReject: return new MarketOrderRejectTransaction();
            case TransactionType.LimitOrder: return new LimitOrderTransaction();
            case TransactionType.LimitOrderReject: return new LimitOrderRejectTransaction();
            case TransactionType.StopOrder: return new StopOrderTransaction();
            case TransactionType.StopOrderReject: return new StopOrderRejectTransaction();
            case TransactionType.MarketIfTouchedOrder: return new MarketIfTouchedOrderTransaction();
            case TransactionType.MarketIfTouchedOrderReject: return new MarketIfTouchedOrderRejectTransaction();
            case TransactionType.TakeProfitOrder: return new TakeProfitOrderTransaction();
            case TransactionType.TakeProfitOrderReject: return new TakeProfitOrderRejectTransaction();
            case TransactionType.StopLossOrder: return new StopLossOrderTransaction();
            case TransactionType.StopLossOrderReject: return new StopLossOrderRejectTransaction();
            case TransactionType.TrailingStopLossOrder: return new TrailingStopLossOrderTransaction();
            case TransactionType.TrailingStopLossOrderReject: return new TrailingStopLossOrderRejectTransaction();
            case TransactionType.OrderFill: return new OrderFillTransaction();
            case TransactionType.OrderCancel: return new OrderCancelTransaction();
            case TransactionType.OrderCancelReject: return new OrderCancelRejectTransaction();
            case TransactionType.OrderClientExtensionsModify: return new OrderClientExtensionsModifyTransaction();
            case TransactionType.OrderClientExtensionsModifyReject: return new OrderClientExtensionsModifyRejectTransaction();
            case TransactionType.TradeClientExtensionsModify: return new TradeClientExtensionsModifyTransaction();
            case TransactionType.TradeClientExtensionsModifyReject: return new TradeClientExtensionsModifyRejectTransaction();
            case TransactionType.MarginCallEnter: return new MarginCallEnterTransaction();
            case TransactionType.MarginCallExtend: return new MarginCallExtendTransaction();
            case TransactionType.MarginCallExit: return new MarginCallExitTransaction();
            case TransactionType.DelayedTradeClosure: return new DelayedTradeClosureTransaction();
            case TransactionType.DailyFinancing: return new DailyFinancingTransaction();
            case TransactionType.ResetResettablePL: return new ResetResettablePLTransaction();
            default: return new Transaction();
         }
      }
   }
}
