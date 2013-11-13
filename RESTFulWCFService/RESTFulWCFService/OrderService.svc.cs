using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace RESTFulWCFService
{
    public class OrderService : IOrderService
    {
        public string GetOrderTotal(string OrderID)
        {
            string orderTotal = string.Empty;

            try
            {
                XDocument doc = XDocument.Load("C:\\Orders.xml");

                orderTotal =
                    (from result in doc.Descendants("DocumentElement")
                    .Descendants("Orders")
                     where result.Element("OrderID").Value == OrderID.ToString()
                     select result.Element("OrderTotal").Value)
                    .FirstOrDefault<string>();

            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return orderTotal;
        }

        public OrderContract GetOrderDetails(string OrderID)
        {
            OrderContract order = new OrderContract();

            try
            {
                XDocument doc = XDocument.Load("C:\\Orders.xml");

                IEnumerable<XElement> orders =
                         (from result in doc.Descendants("DocumentElement")
                             .Descendants("Orders")
                          where result.Element("OrderID").Value == OrderID.ToString()
                          select result);

                order.OrderID = orders.ElementAt(0).Element("OrderID").Value;
                order.OrderDate = orders.ElementAt(0).Element("OrderDate").Value;
                order.ShippedDate = orders.ElementAt(0).Element("ShippedDate").Value;
                order.ShipCountry = orders.ElementAt(0).Element("ShipCountry").Value;
                order.OrderTotal = orders.ElementAt(0).Element("OrderTotal").Value;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return order;
        }

        public bool PlaceOrder(OrderContract order)
        {
            try
            {
                XDocument doc = XDocument.Load("C:\\Orders.xml");

                doc.Element("DocumentElement").Add(
                        new XElement("Products",
                        new XElement("OrderID", order.OrderID),
                        new XElement("OrderDate", order.OrderDate),
                        new XElement("ShippedDate", order.ShippedDate),
                        new XElement("ShipCountry", order.ShipCountry),
                        new XElement("OrderTotal", order.OrderTotal)));

                doc.Save("C:\\Orders.xml");
            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return true;
        }

        public string GetOrderHello()
        {
            return "Hello";
        }


        public List<OrderContract> GetOrderTotal2()
        {
            XDocument doc = XDocument.Load("C:\\Orders.xml");
            List<OrderContract> orderlist = doc.Descendants("Orders").Select(d =>
                new OrderContract
                {
                    OrderID = d.Element("OrderID").Value,
                    OrderDate = d.Element("OrderDate").Value,
                    ShippedDate = d.Element("ShippedDate").Value,
                    ShipCountry = d.Element("ShipCountry").Value,
                    OrderTotal = d.Element("OrderTotal").Value
                }).ToList();
            return orderlist;
        }
    }
}
