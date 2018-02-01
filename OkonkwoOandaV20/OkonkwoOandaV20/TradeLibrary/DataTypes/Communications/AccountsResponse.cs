using OkonkwoOandaV20.TradeLibrary.DataTypes.Account;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class AccountsResponse : Response
	{
		public List<AccountProperties> accounts;
	}
}
