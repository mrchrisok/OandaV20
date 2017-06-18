using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-ep/
   /// </summary>
   public partial class Rest20
   {
      /// <summary>
      /// Posts an order on the given account with the given parameters
      /// </summary>
      /// <param name="account">the account to post on</param>
      /// <param name="request">the order request to post</param>
      /// <returns>PostOrderResponse with details of the results (throws if if fails)</returns>
      public static async Task<OrderPostResponse> PostOrderAsync(string account, IOrderRequest request)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/orders";

         Dictionary<string, IOrderRequest> order = new Dictionary<string, IOrderRequest>();
         order.Add("order", request);

         string body = ConvertToJSON(order);
         var response = await MakeRequestWithJSONBody<OrderPostResponse>("POST", body, requestString);

         return response;
      }

      /// <summary>
      /// Retrieves the list of orders belonging to the account
      /// </summary>
      /// <param name="account">the account to retrieve the list for</param>
      /// <param name="orderIDs">the order IDs to retrieve</param>
      /// <param name="requestParams">optional additional parameters for the request (name, value pairs)</param>
      /// <returns>List of Order objects (or empty list, if no orders)</returns>
      public static async Task<List<IOrder>> GetOrderListAsync(string account, List<string> orderIDs = null, Dictionary<string, string> requestParams = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/orders";

         if (requestParams == null) requestParams = new Dictionary<string, string>();

         // order IDs should only be in the list
         if (requestParams.ContainsKey("ids")) requestParams.Remove("ids");

         if (orderIDs != null)
         {
            string idsParam = GetCommaSeparatedList(orderIDs);
            requestParams.Add("ids", Uri.EscapeDataString(idsParam));
         }

         OrdersResponse response = await MakeRequestAsync<OrdersResponse>(requestString, "GET", requestParams);

         var orders = new List<IOrder>();
         orders.AddRange(response.orders);

         return orders;
      }

      /// <summary>
      /// Retrieves the list of open orders belonging to the account
      /// </summary>
      /// <param name="account">the account to retrieve the list for</param>
      /// <param name="requestParams">optional additional parameters for the request (name, value pairs)</param>
      /// <returns>List of Order objects (or empty list, if no orders)</returns>
      public static async Task<List<IOrder>> GetPendingOrderListAsync(string account)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/pendingOrders";

         OrdersResponse response = await MakeRequestAsync<OrdersResponse>(requestString);

         var orders = new List<IOrder>();
         orders.AddRange(response.orders);

         return orders;
      }

      /// <summary>
      /// Retrieves the details for a given order ID
      /// </summary>
      /// <param name="account">the account that the order belongs to</param>
      /// <param name="orderId">the id of the order to retrieve</param>
      /// <returns>Order object containing the order details</returns>
      public static async Task<IOrder> GetOrderDetailsAsync(string account, long orderId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/orders/" + orderId;

         var response = await MakeRequestAsync<OrderResponse>(requestString);

         return response.order;
      }

      /// <summary>
      /// Replace an order by simultaneously cancelling it and replacing it with the given order request
      /// </summary>
      /// <param name="account">the account to post on</param>
      /// <param name="orderId">the order to cancel</param>
      /// <param name="request">the replacement order request to post</param>
      /// <returns>PostOrderResponse with details of the results (throws if if fails)</returns>
      public static async Task<OrderCancelReplaceResponse> CancelReplaceOrderAsync(string account, long orderId, IOrderRequest request)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/orders/" + orderId;

         Dictionary<string, IOrderRequest> order = new Dictionary<string, IOrderRequest>();
         order.Add("order", request);

         string body = ConvertToJSON(order);
         var response = await MakeRequestWithJSONBody<OrderCancelReplaceResponse>("PUT", body, requestString);

         return response;
      }

      /// <summary>
      /// Cancel the order with the given order ID
      /// </summary>
      /// <param name="account">the account that the order belongs to</param>
      /// <param name="orderId">the id of the order to retrieve</param>
      /// <returns>Order object containing the order details</returns>
      public static async Task<OrderCancelResponse> CancelOrderAsync(string account, long orderId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/orders/" + orderId + "/cancel";

         var response = await MakeRequestAsync<OrderCancelResponse>(requestString, "PUT");

         return response;
      }

      /// <summary>
      /// Modify the Client Extensions for the order in the given account.
      /// </summary>
      /// <param name="account">the account to post on</param>
      /// <param name="orderId">the order to update extensions for</param>
      /// <param name="extensions">the updated extensions for the order</param>
      /// <returns>PostOrderResponse with details of the results (throws if if fails)</returns>
      public static async Task<OrderClientExtensionsModifyResponse> ModifyOrderClientExtensionsAsync(string account, long orderId, ClientExtensions orderExtensions, ClientExtensions tradeExtensions = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/orders/" + orderId + "/clientExtensions";

         Dictionary<string, ClientExtensions> extensions = new Dictionary<string, ClientExtensions>();
         extensions.Add("clientExtensions", orderExtensions);

         if(tradeExtensions != null)
            extensions.Add("tradeClientExtensions", tradeExtensions);

         var response = await MakeRequestWithJSONBody<OrderClientExtensionsModifyResponse, Dictionary<string, ClientExtensions>>("PUT", extensions, requestString);

         return response;
      }
   }
}
