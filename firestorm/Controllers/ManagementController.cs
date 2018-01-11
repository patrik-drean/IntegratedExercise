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
    [Authorize(Users = "Management")]
    public class ManagementController : Controller
    {
        private Thunderstorm db = new Thunderstorm();

        // GET: Management
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SummaryReport()
        {
            List <CompanyWorkOrders> collect = new List<CompanyWorkOrders>();
            CompanyWorkOrders wo = new CompanyWorkOrders();

            var SumRpts = db.Database.SqlQuery<int>("SELECT wo.OrderID FROM SummaryReport sr RIGHT JOIN WorkOrder wo ON sr.OrderID = wo.OrderID INNER JOIN Company c ON c.CompanyID = wo.CompanyID INNER JOIN CompoundSample cs ON cs.OrderID = wo.OrderID  INNER JOIN SampleTest st ON st.LT = cs.LT AND st.SequenceCode = cs.SequenceCode WHERE st.CompletedTest = 1 AND wo.OrderID NOT IN(SELECT wo.OrderID FROM WorkOrder wo INNER JOIN CompoundSample cs ON cs.OrderID = wo.OrderID INNER JOIN SampleTest st ON st.LT = cs.LT AND st.SequenceCode = cs.SequenceCode WHERE st.CompletedTest = 0) AND sr.OrderID IS NULL GROUP BY wo.OrderID").ToList();
            
            foreach(int item in SumRpts)
            {
                wo.WorkOrder = db.WorkOrders.Find(item);

                wo.Company = db.Companies.Find(wo.WorkOrder.CompanyID);

                collect.Add(wo);
            }
            
            
            return View(collect);
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

        public ActionResult ManageTickets()
        {
            var tickets = db.Tickets.Include(t => t.Priority).Include(t => t.User).Include(t => t.WorkOrder);
            return View(tickets.ToList());
        }

        public ActionResult ResolveTicket(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriorityName = new SelectList(db.Priorities, "PriorityName", "PriorityName", ticket.PriorityName);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", ticket.UserID);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments", ticket.OrderID);
            return View(ticket);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResolveTicket([Bind(Include = "TicketID, DateSubmitted, DateResolved, PriorityName, OrderID, UserID, Comment, Response")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.DateResolved = DateTime.Now;

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageTickets");
            }
            ViewBag.PriorityName = new SelectList(db.Priorities, "PriorityName", "PriorityName", ticket.PriorityName);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", ticket.UserID);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments", ticket.OrderID);
            return View(ticket);
        }

    }
}