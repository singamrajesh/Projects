using WebApplication1.Models;
using WebApplication1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation

            // Return Home page.
            public ActionResult Index()
            {
                return View();
            }

            //Return Register view
            public ActionResult Register()
            {
                return View();
            }

            //The form's data in Register view is posted to this method. 
            //We have binded the Register View with Register ViewModel, so we can accept object of Register class as parameter.
            //This object contains all the values entered in the form by the user.
            [HttpPost]
            public ActionResult SaveRegisterDetails(Register registerDetails)
            {
                //We check if the model state is valid or not. We have used DataAnnotation attributes.
                //If any form value fails the DataAnnotation validation the model state becomes invalid.
                if (ModelState.IsValid)
                {
                    //create database context using Entity framework 
                    using (var databaseContext = new testdb1Entities1())
                    {
                        //If the model state is valid i.e. the form values passed the validation then we are storing the User's details in DB.
                        emp reglog = new emp();

                        //Save all details in RegitserUser object

                        reglog.eid= registerDetails.Email;
                        reglog.ename = registerDetails.Name;
                        reglog.epassword = registerDetails.Password;
                        reglog.esal = registerDetails.Salary;

                        databaseContext.emps.Add(reglog);
                        databaseContext.SaveChanges();

                    //Calling the SaveDetails method which saves the details.
                    // databaseContext.emps.Add(reglog);
                    //                     databaseContext.SaveRegisterDetails(reglog);


                }

                    ViewBag.Message = "User Details Saved";
                    return View("Register");
                }
                else
                {

                    //If the validation fails, we are returning the model object with errors to the view, which will display the error messages.
                    return View("Register", registerDetails);
                }
            }


            public ActionResult Login()
            {
                return View();
            }

            //The login form is posted to this method.
            [HttpPost]
            public ActionResult Login(LoginViewModel model)
            {
                //Checking the state of model passed as parameter.
                if (ModelState.IsValid)
                {

                    //Validating the user, whether the user is valid or not.
                    var isValidUser = IsValidUser(model);

                    //If user is valid & present in database, we are redirecting it to Welcome page.
                    if (isValidUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //If the username and password combination is not present in DB then error message is shown.
                        ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                        return View();
                    }
                }
                else
                {
                    //If model state is not valid, the model with error message is returned to the View.
                    return View(model);
                }
            }

            //function to check if User is valid or not
            public emp IsValidUser(LoginViewModel model)
            {
                using (var dataContext = new testdb1Entities1())
                {
                //Retireving the user details from DB based on username and password enetered by user.
                   emp user = dataContext.emps.Where(query => query.eid.Equals(model.Email) && query.epassword.Equals(model.Password)).SingleOrDefault();
                      //If user is present, then true is returned.
                      if (user == null)
                          return null;
                      //If user is not present false is returned.
                      else
                          return user;
                          
                
                }
            }


            public ActionResult Logout()
            {
                FormsAuthentication.SignOut();
                Session.Abandon(); // it will clear the session at the end of request
                return RedirectToAction("Index");
            }
        }
    }