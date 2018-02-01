using OkonkwoOandaV20.TradeLibrary.DataTypes.Account;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class AccountChangesResponse : Response
   {
      public AccountChanges changes { get; set; }
      public AccountChangesState state { get; set; }
   }
}
