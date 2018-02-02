namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class ErrorResponse : Response, IErrorResponse
   {
      public string errorCode { get; set; }
      public string errorMessage { get; set; }
   }

   public interface IErrorResponse
   {
      string errorCode { get; set; }
      string errorMessage { get; set; }
   }
}
