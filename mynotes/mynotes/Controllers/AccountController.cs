using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mynotes.Database;
using mynotes.Models;

namespace mynotes.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AccountController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _databaseContext.Account.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password );

                if(account != null && account.IsActive == 1)
                {
                    var user = _databaseContext.Users.FirstOrDefault( x=> x.UserId == account.UserId );
                    if (user != null)
                    {
                        HttpContext.Session.SetString("IsLoggedIn", "true");
                        HttpContext.Session.SetString("Username", account.UserName);

                        //string control = HttpContext.Session.GetString("IsLoggedIn");
                        //ViewBag.IsLoggedId = "true";
                        //ViewBag.FirstName = user.FirstName;
                        if (user.IsAdmin == 1)
                        {
                            user.IsAdmin = 1;
                            _databaseContext.SaveChanges();

                            return RedirectToAction("UserList", "User");
                        }
                        else
                        {
                            
                            return RedirectToAction("Index", new RouteValueDictionary(
                                new { controller = "User", action = "Index", accountId = account.AccountId }));

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                }


            }
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("IsLoggedIn", "false");
            // HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult SingUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingUp(SingupViewModel model)
        {
            if (ModelState.IsValid)
            {

                var accounts =  _databaseContext.Account.AsQueryable().ToList();
                var users = _databaseContext.Users.AsQueryable().ToList();
                var isUserNameExist = accounts.Where(x => x.UserName == model.UserName).ToList(); //_databaseContext.Users.All(u => u.UserName != model.UserName);

                if (isUserNameExist != null && isUserNameExist.Count > 0)
                {
                    ModelState.AddModelError(nameof(SingupViewModel.UserName), "Bu kullanıcı adı zaten kullanılmaktadır.");
                    return View(model);
                }

                var isUserExist = users.Where(x => x.MailAddress == model.MailAddress).ToList();
               

                if (isUserExist != null && isUserExist.Count > 0)
                {
                    var newAccount = new Account
                    {
                        UserId = isUserExist.FirstOrDefault().UserId,
                        Password = model.Password,
                        UserName = model.UserName,
                        IsActive = 1
                    };

                    _databaseContext.Account.Add(newAccount);

                }
                else
                {
                    var newUser = new Users
                    {
                        FirstName = model.FirstName,
                        SurName = model.SurName,
                        MailAddress = model.MailAddress,
                        IsActive = 1,
                        IsAdmin = 0,
                        CreateTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now,
                    };
                     _databaseContext.Users.Add(newUser);
                    _databaseContext.SaveChanges();



                    var newAccount = new Account
                    {
                        UserId =  _databaseContext.Users.AsQueryable().ToList().Where(x => x.MailAddress == model.MailAddress).FirstOrDefault().UserId,
                        Password = model.Password,
                        UserName = model.UserName,
                        IsActive = 1
                    };

                    _databaseContext.Account.Add(newAccount);

                }

                _databaseContext.SaveChanges();

              
                return RedirectToAction("Login");
            }

          
            return View(model);
        }


    }
}
