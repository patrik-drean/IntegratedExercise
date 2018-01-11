using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using firestorm.Models;
using firestorm.DAL;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Security;

namespace firestorm.Controllers
{
    [Authorize(Users = "Sales")]
    public class SalesController : Controller
    {
        private Thunderstorm db = new Thunderstorm();

        // GET: Sales
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult CreateTicket()
        {
            ViewBag.PriorityName = new SelectList(db.Priorities, "PriorityName", "PriorityName");
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "OrderID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTicket([Bind(Include = "PriorityName,OrderID,Comment")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.DateSubmitted = DateTime.Now;
                ticket.UserID = Convert.ToInt32(Request.Cookies["UserID"].Value);
                ticket.TicketID = db.Tickets.Max(p => p.TicketID) + 1;

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PriorityName = new SelectList(db.Priorities, "PriorityName", "PriorityName", ticket.PriorityName);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "OrderID", ticket.OrderID);
            return View(ticket);
        }
    }
}