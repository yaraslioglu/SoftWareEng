using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTFulWCFService
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetOrderHello",
            ResponseFormat = WebMessageFormat.Json)]
        string GetOrderHello();


        [OperationContract]
        [WebGet(UriTemplate = "/GetOrderTotal/{OrderID}",
            ResponseFormat = WebMessageFormat.Json)]
        string GetOrderTotal(string OrderID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetOrderTotal2",
            ResponseFormat = WebMessageFormat.Json)]
        List<OrderContract> GetOrderTotal2();



        [OperationContract]
        [WebGet(UriTemplate = "/GetOrderDetails/{OrderID}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        OrderContract GetOrderDetails(string OrderID);

        [OperationContract]
        [WebInvoke(UriTemplate = "/PlaceOrder",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        bool PlaceOrder(OrderContract order);
    }
    [DataContract]
    public class OrderContract
    {
        [DataMember]
        public string OrderID { get; set; }

        [DataMember]
        public string OrderDate { get; set; }

        [DataMember]
        public string ShippedDate { get; set; }

        [DataMember]
        public string ShipCountry { get; set; }

        [DataMember]
        public string OrderTotal { get; set; }
    }
}
