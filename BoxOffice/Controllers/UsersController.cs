using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Events;
using BoxOffice.Models;
using System.Web.Security;
using BoxOffice.ActionFilters;

namespace BoxOffice.Controllers
{ 
    [Authorize(Roles = "User")]
    [JsonRequestBehavior]
    public class UsersController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();

        //
        // GET: /Users/
        
        /// <summary>
        /// Get a list of all users
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("Queue");
        }

        //
        // GET: /Users/Profile/id

        /// <summary>
        /// Returns the Profile for a user
        /// </summary>
        /// <param name="id">The UserID whose profile is requested</param>
        /// <returns>View(user)</returns>
        public ViewResult Queue()
        {
            return View(model: db.Users.First(a => a.Username == User.Identity.Name));
        }

        //
        // GET /Users/Profile/

        /// <summary>
        /// shows the user's profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Profile()
        {
            // set FormAction
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            ViewBag.FormAction = actionName;

            return View(model: db.Users.First(a => a.Username == User.Identity.Name));
        }

        //
        // POST: /Users/Profile/

        /// <summary>
        /// Processes a user's profile for persistence
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Profile(User model)
        {
            if (ModelState.IsValid)
            {
                // get the user object
                var user = db.Users.First(u => u.UserID == model.UserID);

                // update information
                user.City = model.City;
                user.DateOfBirth = model.DateOfBirth;
                user.Email = model.Email;
                user.Name = model.Name;
                user.Number = model.Number;
                user.Phone = model.Phone;
                user.Street = model.Street;
                user.Surname = model.Surname;
                user.Zip = model.Zip;

                // save changes
                db.SaveChanges();

                return View();
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Users/History
        /// <summary>
        /// displays a user's rental history
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult History()
        {
            var theUser = db.Users.First(a => a.Username == User.Identity.Name);

            return
                View(
                    theUser.Queue.Where(rental => rental.QueuePosition == null && rental.Hide == false).OrderBy(rental => rental.DateSent).
                        ToList());
        }

        //
        // GET: /Users/Unqueue/{id}

        /// <summary>
        /// Unqueues a movie
        /// </summary>
        /// <param name="id">RentalID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Unqueue(int id)
        {
            try
            {
                var rental = db.Rentals.First(r => r.RentalID == id);

                // unqueue
                rental.Hide = true;
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                return Json(new {success = false, error = e.Message});
            }

            return Json(new { success = true });
        }

        //
        // GET: /Movies/Return/{id}

        /// <summary>
        /// handles a users return of a dvd
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Return(int id)
        {
            var theReturn = db.Rentals.First(d => d.RentalID == id);

            if (Dispatch.ReturnDVD(theReturn))
            {
                return Json(new {success = true});
            }
            return Json(new {success = false});
        }

        //
        // GET: /Users/Comments

        /// <summary>
        /// shows the comments view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Comments()
        {
            return View(db.Users.First(a => a.Username == User.Identity.Name));
        }

        //
        // GET: /Users/EditComment/

        /// <summary>
        /// updates a comments text
        /// </summary>
        /// <param name="id"></param>
        /// <param name="edit"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult EditComment(int id, string edit)
        {
            try
            {
                var comment = db.Comments.First(c => c.CommentID == id);

                comment.Message = edit;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new {success = false, error = e.Message});
            }
            return Json(new {success = true});
        }

        #region login
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonLogin

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonLogin(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction(controllerName: "Admin", actionName: "Index");
                    }
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            foreach (var cookie in Request.Cookies.AllKeys)
            {
                Request.Cookies.Remove(cookie);
            }
            foreach (var cookie in Response.Cookies.AllKeys)
            {
                Response.Cookies.Remove(cookie);
            }

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;

                // check if username is unique
                if (UserNameIsUnique(model.UserName))
                {
                    // Attempt to register the user
                    Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                    Roles.AddUserToRole(model.UserName, "User");
                }
                else
                {
                    createStatus = MembershipCreateStatus.DuplicateUserName;
                }

                if (createStatus == MembershipCreateStatus.Success)
                {
                    var newUser = new User
                    {
                        City = string.Empty,
                        Comments = new List<Comment>(),
                        DateOfBirth = null,
                        Email = model.Email,
                        Messages = new List<Message>(),
                        Name = string.Empty,
                        Number = string.Empty,
                        Phone = 0,
                        Queue = new List<Rental>(),
                        Ratings = new List<Rating>(),
                        Street = string.Empty,
                        Surname = string.Empty,
                        Username = model.UserName,
                        Zip = string.Empty
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return Json(new { success = true, redirect = "/Users/Profile" });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;

                // check for username uniqueness
                if (UserNameIsUnique(model.UserName))
                {
                    // Attempt to register the user
                    Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                    Roles.AddUserToRole(model.UserName, "User");
                }
                else
                {
                    // username exists so throw error
                    createStatus = MembershipCreateStatus.DuplicateUserName;
                }

                if (createStatus == MembershipCreateStatus.Success)
                {
                    var newUser = new User
                    {
                        City = string.Empty,
                        Comments = new List<Comment>(),
                        DateOfBirth = null,
                        Email = model.Email,
                        Messages = new List<Message>(),
                        Name = string.Empty,
                        Number = string.Empty,
                        Phone = 0,
                        Queue = new List<Rental>(),
                        Ratings = new List<Rating>(),
                        Street = string.Empty,
                        Surname = string.Empty,
                        Username = model.UserName,
                        Zip = string.Empty
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return RedirectToAction("Profile", "Users");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        [Authorize]       
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        #endregion

        #region Helper Methods

        private bool UserNameIsUnique(string UserNameToCheck)
        {
            var aUser = from user in db.Users
                        where user.Username == UserNameToCheck
                        select user;

            return !aUser.Any();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}