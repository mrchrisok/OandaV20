namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class StopLossOrder : ExitOrder
   {
      public decimal price { get; set; }
      public string positionFill { get; set; }
   }
}
