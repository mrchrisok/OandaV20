using System;

namespace OANDAV20.REST20.Framework
{
   public class IsOptionalAttribute : Attribute
   {
      public override string ToString()
      {
         return "Is Optional";
      }
   } 
}
