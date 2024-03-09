using Microsoft.AspNetCore.Mvc;
using mynotes.Database;
using mynotes.Models;
using System.Security.Principal;

namespace mynotes.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public UserController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IActionResult Index(string accountId)
        {
            ViewBag.AccountId = accountId;
            return View();
        }



        public IActionResult UserList()
        {

            var nonAdminUsers = _databaseContext.Users.Where(u => u.IsAdmin == 0 && u.IsActive == 1).ToList();
            return View(nonAdminUsers);
        }
        [HttpPost]
        public IActionResult UserDelete(int userId)
        {
            var user = _databaseContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.IsActive = 0; 
                _databaseContext.SaveChanges();
            }

            return RedirectToAction("UserList");
        }

        public IActionResult UserEdit(int accountId)
        {
            var account = _databaseContext.Account.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null)
                return NotFound();

            var user = _databaseContext.Users.FirstOrDefault(u => u.UserId == account.UserId);
            if (user == null)
                return NotFound();

            UserEditRequestModel model = new UserEditRequestModel();
            model.UserId = user.UserId;
            model.AccountId = account.AccountId;
            model.MailAddress = user.MailAddress;
            model.FirstName = user.FirstName;
            model.SurName = user.SurName;
            model.UserName = account.UserName;
            model.Password = account.Password;
            
            return View(model);
        }

        [HttpPost]
        public IActionResult UserEdit(UserEditRequestModel updatedUser) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = _databaseContext.Users.Find(updatedUser.UserId);
                    if (existingUser == null)
                        return NotFound();

                    var existingAccount = _databaseContext.Account.Find(updatedUser.AccountId);
                    if (existingAccount == null)
                        return NotFound();

                    existingUser.FirstName = updatedUser.FirstName;
                    existingUser.SurName = updatedUser.SurName;
                    existingUser.MailAddress = updatedUser.MailAddress;
                    existingUser.LastUpdateTime = DateTime.Now;

                    existingAccount.UserName = updatedUser.UserName;
                    existingAccount.Password = updatedUser.Password;

                    _databaseContext.SaveChanges();

                    TempData["SuccessMessage"] = "Kullanıcı başarıyla güncellendi.";
                    return RedirectToAction("Index", new RouteValueDictionary(
                        new { controller = "User", action = "Index", accountId = updatedUser.AccountId })); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
                }
            }

            return View(updatedUser);
        }

    }
}
