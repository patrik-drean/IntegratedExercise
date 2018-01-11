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
    
    public class HomeController : Controller
    {
        private Thunderstorm db = new Thunderstorm();
        static User PendedUser = new User();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            String email = form["Email address"].ToString();
            String password = form["Password"].ToString();
            User currentUser = null;

            try
            {
                currentUser = db.Database.SqlQuery<User>(
                            "Select * " +
                            "FROM [User] " +
                            "WHERE Email = '" + email + "' AND " +
                            "[Password] = '" + password + "'").First();
            }
            catch(Exception e)
            {
                return View();
            }
            
           
            

            if (currentUser != null)
            {
                String role = db.Database.SqlQuery<Role>("SELECT * FROM Role WHERE RoleID = " + currentUser.RoleID).First().Name;
                FormsAuthentication.SetAuthCookie(role, rememberMe);

                //sets the user id cookie
                HttpCookie myCookie = new HttpCookie("UserID", currentUser.UserID.ToString());
                Response.Cookies.Add(myCookie);
                //this is how to read from the cookie
                    //int UserID = Convert.ToInt32(Request.Cookies["UserID"].Value);

                return RedirectToAction("Index", role, null);
            }
            else
            {
                return View();
            }
        }


        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount([Bind(Include = "UserID,FirstName,LastName,Phone,Email,Password,RoleID,CompanyID")] User user, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                // Assign user to their role
                user.RoleID = 1;

                // Assign PK
                user.UserID = db.Users.ToList().Max(x => x.UserID) + 1;

                // Determine if company is already in db
                List <Company> Companies = db.Companies.ToList();
                Boolean CompanyExists = false;

                foreach(var Item in Companies)
                {
                    if (Item.Name.Equals(form["CompanyName"].ToString()))
                    {
                        user.CompanyID = Item.CompanyID;
                        CompanyExists = true;
                    }
                }

                // Add custoemr to the database if the company exists
                if (CompanyExists)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Customer");
                }

                else
                {
                    PendedUser = user;
                    return RedirectToAction("CreateCompany", new { CompanyName = form["CompanyName"].ToString() });
                }
            }

            return View(user);
        }

        public ActionResult CreateCompany(string CompanyName)
        {
            ViewBag.CompanyName = CompanyName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompany([Bind(Include = "CompanyID, Name, Address, City, State, Zip, Balance")] Company company)
        {
            if (ModelState.IsValid)
            {
                // Assign company's pk
                company.CompanyID = db.Companies.Max(x => x.CompanyID) + 1;

                // Give 0 balance
                company.Balance = 0;

                // Assign the user the company id
                PendedUser.CompanyID = company.CompanyID;

                // Update db with new company and user
                db.Companies.Add(company);
                db.Users.Add(PendedUser);
               
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.CompanyName = company.Name;
            return View(company);
        }

        public ActionResult UnderConstruction()
        {

            return View();
        }


    }
}