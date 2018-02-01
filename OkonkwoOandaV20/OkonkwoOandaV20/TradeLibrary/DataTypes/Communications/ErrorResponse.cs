namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public abstract class ErrorResponse : Response
   {
      public string errorCode { get; set; }
      public string errorMessage { get; set; }
   }
}
