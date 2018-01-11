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
    [Authorize(Users = "Lab")]
    public class LabController : Controller
    {
        // GET: Lab

        private Thunderstorm db = new Thunderstorm();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OperForecast()
        {
            List<OperationForecastCompoundSample> CompoundSamples = db.Database.SqlQuery<OperationForecastCompoundSample>("SELECT  OrderID, DateDue FROM CompoundSample WHERE ReceivedBY IS NULL GROUP BY OrderID, DateDue").ToList();
            return View(CompoundSamples);
        }
        public ActionResult DisplayTests()
        {
            IEnumerable<NewTestTube> test= db.Database.SqlQuery<NewTestTube>("SELECT * " +
                "FROM Assay, Test, CompoundSample, AssayTest, SampleTest " +
                "WHERE Test.TestID = AssayTest.TestID AND AssayTest.AssayID = Assay.AssayID " +
                "AND Assay.AssayID = CompoundSample.AssayID AND AssayTest.AssayID = CompoundSample.AssayID " +
                "AND Assay.AssayID = SampleTest.AssayID AND SampleTest.AssayID = CompoundSample.AssayID " +
                "AND AssayTest.AssayID = SampleTest.AssayID AND Test.TestID = SampleTest.TestID " +
                "AND AssayTest.TestID = SampleTest.TestID");
            return View(test);
        }
        //Edit
        public ActionResult CreateTTube(int? id, int? lt, int? sc, string asid, int? ttid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (lt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (sc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (asid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ttid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleTest atest = db.SampleTests.Find(ttid);
            return View(atest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTTube([Bind(Include = "TestTubeID,ScheduledDate,Concentration,LT,SequenceCode,AssayID,TestID")] SampleTest test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(test);
        }
        
        public ActionResult UpdateTest()
        {
            return View();
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
                return RedirectToAction("DisplayTests");
            }
            ViewBag.PriorityName = new SelectList(db.Priorities, "PriorityName", "PriorityName", ticket.PriorityName);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", ticket.UserID);
            ViewBag.OrderID = new SelectList(db.WorkOrders, "OrderID", "Comments", ticket.OrderID);
            return View(ticket);
        }

      
    }
}