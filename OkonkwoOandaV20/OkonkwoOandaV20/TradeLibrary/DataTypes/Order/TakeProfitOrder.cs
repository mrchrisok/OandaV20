namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class TakeProfitOrder : ExitOrder
   {
      public decimal price { get; set; }
      public string positionFill { get; set; }
   }
}
