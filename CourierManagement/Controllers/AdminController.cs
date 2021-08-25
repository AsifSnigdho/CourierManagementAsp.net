using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourierManagement.Models;
using CrystalDecisions.CrystalReports.Engine;
using FastMember;

namespace CourierManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly CourierManagementEntities db;


        int SearchID = 0;
        public AdminController()
        {

            db = new CourierManagementEntities();
        }
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin userModel)
        {
            using (CourierManagementEntities db = new CourierManagementEntities())
            {
                var userDetails = db.Admins.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password";
                    return View("Login", userModel);
                }
                else
                {
                    return RedirectToAction("SuccededLogin");
                }
            }

                
        }
        
        public ActionResult SuccededLogin(string List)
        {
            if (List == null || List == "")
            {
                List = "0";
            }

            return View(AllList(List));

            

                
        }
        public IEnumerable<OrderList> AllList(string List)
        {
            SearchID = int.Parse(List);

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

        

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(OrderList info)
        //{
        //    try
        //    {
        //        using (CourierManagementEntities db = new CourierManagementEntities())
        //        {
        //            db.OrderLists.Add(info);
        //            db.SaveChanges();
        //            return RedirectToAction("SuccededLogin");
        //        }

        //    }
        //    catch
        //    {
        //        return View();

        //    }
            
        //}

        public ActionResult Edit(int id)
        {
            using (CourierManagementEntities db = new CourierManagementEntities())
            {
                return View(db.OrderLists.Where(x => x.TrackID==id).FirstOrDefault());
            }
               
        }
        [HttpPost]

        public ActionResult Edit(int id, OrderList EditByID)
        {
            try
            {
                using (CourierManagementEntities db = new CourierManagementEntities())
                {
                    db.Entry(EditByID).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccededLogin");
                }

            }
            catch
            {
                return View();

            }
            
        }

        /*public ActionResult Details()
        {
            return View(id);

        }
        [HttpPost]

        public ActionResult Details(int id)
        {
            using (CourierManagementEntities db = new CourierManagementEntities())
            {
                return View(db.OrderLists.Where(x => x.TrackID == id).FirstOrDefault());
            }

        }*/

        public ActionResult GenerateReport()
        {
            List<OrderList> AllOrders = db.OrderLists.ToList();

            DataTable dt = new DataTable();

            using (var reader = ObjectReader.Create(AllOrders))
            {
                dt.Load(reader);
            }


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "AllDetails.rpt"));

            rd.SetDataSource(dt);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Allorders.pdf");
        }
    }
}