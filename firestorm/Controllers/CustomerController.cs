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
 
    [Authorize(Users = "Customer")]
    public class CustomerController : Controller
    {
        private Thunderstorm db = new Thunderstorm();
        static public WorkOrderCompoundSample workOrderCompoundSample;
        static public User user;
        static public List<int> iTest;
        static public int iTestTubeCounter;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestOrderNumber()
        {
            user = db.Users.Find(Int32.Parse(Request.Cookies["UserID"].Value));
            iTestTubeCounter = db.SampleTests.Max(x => x.TestTubeID);

            workOrderCompoundSample = new WorkOrderCompoundSample();

            workOrderCompoundSample.workOrder = new WorkOrder();
            workOrderCompoundSample.compound = new Compound();


            workOrderCompoundSample.workOrder.Discount = 0;
            workOrderCompoundSample.workOrder.CompanyID = user.CompanyID.Value;

            workOrderCompoundSample.workOrder.OrderID = db.WorkOrders.ToList().Max(x => x.OrderID) + 1;

            //Create compound and assign the number
            workOrderCompoundSample.compound.LT = db.Compounds.ToList().Max(x => x.LT) + 1;

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "1", Value = "1", Selected = true });
            items.Add(new SelectListItem { Text = "2", Value = "2" });
            items.Add(new SelectListItem { Text = "3", Value = "3" });
            items.Add(new SelectListItem { Text = "4", Value = "4" });
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "6", Value = "6" });
            items.Add(new SelectListItem { Text = "7", Value = "7" });
            items.Add(new SelectListItem { Text = "8", Value = "8" });
            items.Add(new SelectListItem { Text = "9", Value = "9" });


            ViewBag.CompoundCount = items;
            return View("RequestOrderNumber");
        }



        [HttpPost]
        public ActionResult RequestOrderNumber(FormCollection form)
        {
           
            int CompoundsNeeded = Int32.Parse(form["CompoundCount"]);
            int iCount = 0;
            List<SelectListItem> items = new List<SelectListItem>();
            workOrderCompoundSample.compound.Name = form["compound.Name"];

            items.Add(new SelectListItem { Text = "1", Value = "1" });
            items.Add(new SelectListItem { Text = "2", Value = "2" });
            items.Add(new SelectListItem { Text = "3", Value = "3" });
            items.Add(new SelectListItem { Text = "4", Value = "4" });
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "6", Value = "6" });
            items.Add(new SelectListItem { Text = "7", Value = "7" });
            items.Add(new SelectListItem { Text = "8", Value = "8" });
            items.Add(new SelectListItem { Text = "9", Value = "9" });

            List<SelectListItem> items2 = new List<SelectListItem>();


            items2.Add(new SelectListItem { Text = "1", Value = "1" });
            items2.Add(new SelectListItem { Text = "2", Value = "2" });
            items2.Add(new SelectListItem { Text = "3", Value = "3" });
            items2.Add(new SelectListItem { Text = "4", Value = "4" });
            items2.Add(new SelectListItem { Text = "5", Value = "5" });
            items2.Add(new SelectListItem { Text = "6", Value = "6" });
    

            foreach (var item in items)
            {
                if (Int32.Parse(item.Value) == CompoundsNeeded)
                {
                    item.Selected = true;
                }
            }

            ViewBag.CompoundCount = items;
            ViewBag.TestCount = items2;
            ViewBag.DisplayAssayList = db.Assays.ToList();

            workOrderCompoundSample.compoundSampleSampleTests = new List<CompoundSampleSampleTest>();

            int iSequenceCounter = 0;
            int iSequenceQueryCounter = 0;
            for (iCount = 0; iCount < CompoundsNeeded; iCount++)
            {
                
                workOrderCompoundSample.compoundSampleSampleTests.Add(new CompoundSampleSampleTest());
                workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).compoundSample = new CompoundSample();
                workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).compoundSample.OrderID = workOrderCompoundSample.workOrder.OrderID;
                workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).compoundSample.LT = workOrderCompoundSample.compound.LT;

                 var SequenceNumbers = db.Database.SqlQuery<CompoundSample>("SELECT * FROM CompoundSample WHERE LT = '" + workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).compoundSample.LT + "'").ToList();

                if (SequenceNumbers.Count > 0)
                {
                    iSequenceQueryCounter++;
                    workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).compoundSample.SequenceCode = SequenceNumbers.Max(x => x.SequenceCode) + iSequenceQueryCounter;
                }
                else
                {
                    iSequenceCounter++;
                    workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).compoundSample.SequenceCode = iSequenceCounter;
                }
                }



            return View(workOrderCompoundSample);
        }


        [HttpPost]
        public ActionResult RequestOrderTests(FormCollection form)
        {
            List<string> sTests = form["TestCount"].Split(',').ToList();
            iTest = new List<int>();
            int iCount = 0;
            int iCount1 = 0;

            foreach (var Test in sTests)
            {
                iTest.Add(Int32.Parse(Test));
            }
            
            // Loop to add SampleTests to get the amount of Assays
            foreach (var Compound in workOrderCompoundSample.compoundSampleSampleTests)
            {
                Compound.sampleTests = new List<SampleTest>();
                for(iCount = 0; iCount < iTest.ElementAt(iCount1); iCount++ )
                {
                    Compound.sampleTests.Add(new SampleTest());
                }
                iCount1++;
            }
            return RedirectToAction("RequestOrder", "Customer");
        }



/**********************REQUEST ORDER ************************/





        public ActionResult RequestOrder()
        {
            int iCount = 0;

         
            // Put Assays to list for the view
            ViewBag.Assays = db.Assays.ToList();

            return View(workOrderCompoundSample);
        }

        
        [HttpPost]
        public ActionResult RequestOrder(FormCollection form)
        {
            /*Weight
             *DateDue
             *AssayID
             * Comments
             */
            List<string> sAssays = form["AssayID"].Split(',').ToList();
            int iCount = 0;
            int iCount1 = 0;
            int iCount2 = 0;
            int iCount3 = 0;
            
            foreach(var Compound in workOrderCompoundSample.compoundSampleSampleTests)
            { 
                Compound.compoundSample.DateDue = Convert.ToDateTime(form["DateDue"]);

                // Clear the Sample tests for each compound to determine the amount of samples tests within each assay
                Compound.sampleTests.Clear();
                iCount3 = 0;
                // Loop for all the assays
                for (iCount = 0; iCount < iTest.ElementAt(iCount1); iCount++)
                    {
                        Compound.compoundSample.AssayID = sAssays.ElementAt(iCount);
                        var Tests = db.Database.SqlQuery<AssayTest>("SELECT * FROM AssayTest WHERE AssayID = '" + Compound.compoundSample.AssayID +"'").ToList();
                     
                    // Loop for all the sample tests within each assay
                    for (iCount2 = 0; iCount2 < Tests.Count(); iCount2++)
                    {
                        Compound.sampleTests.Add(new SampleTest());
                        Compound.sampleTests.ElementAt(iCount3).LT = workOrderCompoundSample.compound.LT;
                        Compound.sampleTests.ElementAt(iCount3).SequenceCode = Compound.compoundSample.SequenceCode;

                        iTestTubeCounter++;
                        Compound.sampleTests.ElementAt(iCount3).TestTubeID = iTestTubeCounter;
                        Compound.sampleTests.ElementAt(iCount3).TestID = Tests.ElementAt(iCount2).TestID;
                        Compound.sampleTests.ElementAt(iCount3).AssayID = Compound.compoundSample.AssayID;

                        iCount3++;
                        }
                      
                }
                iCount1++;
            }

            workOrderCompoundSample.workOrder.Comments = form["workOrder.Comments"];

            // Update database tables
            
            // Update Work Order
            db.WorkOrders.Add(workOrderCompoundSample.workOrder);
            db.SaveChanges();


            // Check if Compound is already in table
            var Compounds = db.Compounds.ToList();

            Boolean CompoundUpdate = true;
            foreach (var Item in Compounds)
            {
                if (Item.Name == workOrderCompoundSample.compound.Name)
                {
                    CompoundUpdate = false;
                }
            }
            // Updated Compound
            if (CompoundUpdate)
            {
                db.Compounds.Add(workOrderCompoundSample.compound);
                db.SaveChanges();
            }
           
            // Loop to Update Compound Samples
            foreach (var Item in workOrderCompoundSample.compoundSampleSampleTests)
                {
                    db.CompoundSamples.Add(Item.compoundSample);
                    db.SaveChanges();

                    // Loop to updated Sample Tests
                    foreach (var SampleTest in Item.sampleTests)
                    {
                        db.SampleTests.Add(SampleTest);
                        db.SaveChanges();
                    }
                }


            return RedirectToAction("Confirmation", new {Name = user.FirstName });
        }


        public ActionResult Confirmation(string Name)
        {
            ViewBag.Name = Name;
            return View();
        }


        public ActionResult TrackOrder()
        {
            User user = db.Users.Find(Convert.ToInt32(Request.Cookies["UserID"].Value));

            ViewBag.CompanyName = user.Company.Name;

            var orders = db.Database.SqlQuery<WorkOrder>("SELECT * FROM WorkOrder WHERE CompanyID = " + user.CompanyID).ToList();

            return View(orders);
        }
        public ActionResult ViewOrder(int? OrderID)
        {
            WorkOrderCompoundSample workOrderCompoundSample = new WorkOrderCompoundSample();

            // Find work order
            workOrderCompoundSample.workOrder = db.WorkOrders.Find(OrderID);
            workOrderCompoundSample.compoundSampleSampleTests = new List<CompoundSampleSampleTest>();

            // Find the compund samples with that work order
            List<CompoundSample> dbCompoundSamples = db.Database.SqlQuery<CompoundSample>("SELECT * FROM CompoundSample WHERE OrderID = " + OrderID).ToList();



            int iCount = 0;
            int iCount1 = 0;
            int iTestCount = 0;
            foreach (var CompoundSample in dbCompoundSamples)
            {
                workOrderCompoundSample.compoundSampleSampleTests.Add(new CompoundSampleSampleTest());
                workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).compoundSample = CompoundSample;

                // Find all the sample tests within the compounds of the work order
                List<SampleTest> dbSampleTests = db.Database.SqlQuery<SampleTest>("SELECT * FROM SampleTest WHERE LT = '" + 
                                                                                    dbCompoundSamples.ElementAt(iCount).LT + "' AND SequenceCode =" +
                                                                                    dbCompoundSamples.ElementAt(iCount).SequenceCode).ToList();
                workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).sampleTests = new List<SampleTest>();

                for (iCount1 = 0; iCount1 < dbSampleTests.Count(); iCount1++)
                {
                    workOrderCompoundSample.compoundSampleSampleTests.ElementAt(iCount).sampleTests.Add(dbSampleTests.ElementAt(iCount1));
                    iTestCount++;
                }

                iCount++;
            }

           List<Compound> Compounds = db.Database.SqlQuery<Compound>("SELECT * FROM Compound WHERE LT = '" +workOrderCompoundSample.compoundSampleSampleTests.ElementAt(0).compoundSample.LT + "'").ToList();
            workOrderCompoundSample.compound = Compounds.ElementAt(0);
            ViewBag.Name = db.Users.Find(Convert.ToInt32(Request.Cookies["UserID"].Value)).FirstName;
            ViewBag.CompanyName = db.Companies.Find(workOrderCompoundSample.workOrder.CompanyID).Name;
            ViewBag.TotalTest = iTestCount;

            return View(workOrderCompoundSample);
        }


    }
}