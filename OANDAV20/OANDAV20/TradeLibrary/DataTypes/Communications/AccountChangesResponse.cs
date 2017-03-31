using OANDAV20.TradeLibrary.DataTypes.Account;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class AccountChangesResponse
   {
      public AccountChanges changes;
      public AccountChangesState state;
      public long lastTransactionID;
   }
}
