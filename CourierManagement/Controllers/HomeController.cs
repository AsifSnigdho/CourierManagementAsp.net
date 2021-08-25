using CourierManagement.Models;
using CourierManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CourierManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly CourierManagementEntities db;


        int SearchID = 0;

        public HomeController()
        {

            db = new CourierManagementEntities();
        }
        public ActionResult Index(string searching)
        {
            if(Session["OrderTrackID"]!=null)
            {

                try
                {
                    Response.Write("<script>alert('Your TrackId No: " + Session["OrderTrackID"].ToString() + ". Please note down for further usage.')</script>");
                }
                catch (Exception)
                {

                    throw;
                }
                finally { Session["OrderTrackID"] = ""; }
            }

            if (searching == null || searching == "")
            {
                searching = "0";
            }



            return View(CheckFound(searching));

            //return RedirectToAction("PlaceOrder",, searching);
        }

        //returns list
        public IEnumerable<OrderList> CheckFound(string searching)
        {
            SearchID = int.Parse(searching);

            var FoundList = db.OrderLists.Where(x => x.TrackID == SearchID).ToList();

            if (FoundList.Count > 0)
            {
                return FoundList;
            }
            else
            {
                if (SearchID == 0)
                {
                    ViewBag.SearchAlert = null;
                }
                else
                {
                    ViewBag.SearchAlert = "No match found!";
                }


                var all = db.OrderLists.ToList();
                return all;
            }
        }


        [HttpGet]
        public ActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrder(OrderList placedOrder)
        {

            OrderList obj = new OrderList();
            obj.CustomerName = placedOrder.CustomerName;
            obj.CustomerPhoneNumber = placedOrder.CustomerPhoneNumber;
            obj.ReceiverName = placedOrder.ReceiverName;
            obj.ReceiverPhoneNumber = placedOrder.ReceiverPhoneNumber;
            obj.ReceiverAddress = placedOrder.ReceiverAddress;
            obj.OrderDate = DateTime.Now;
            obj.Status = "Pending";

            db.OrderLists.Add(obj);
            db.SaveChanges();

            Session["OrderTrackID"] = obj.TrackID.ToString();
            
            return RedirectToAction("Index");
        }

    }
}