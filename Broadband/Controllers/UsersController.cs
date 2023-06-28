using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Broadband.Models;
using Broadband.Data;

namespace Broadband.Controllers
{
    [Authorize(Roles="Admin")]
    public class UsersController : Controller
    {
        UserManager<IdentityUser> _userManager;
        UserMessageDbContext _db;


        
        public UsersController(UserManager<IdentityUser> userManager, UserMessageDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()

        {
            return View(_db.LoginData.ToList());
        }

        public async Task<IActionResult>Edit(string id)
        {
            var user=_db.LoginData.FirstOrDefault(c=>c.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult>Edit(Login user)
        {
            var userInfo = _db.LoginData.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }

            userInfo.Name = user.Name;
            userInfo.Address = user.Address; ;
            userInfo.PhoneNumber=user.PhoneNumber; 
            userInfo.Packages=user.Packages;
            var result = await _userManager.UpdateAsync(userInfo);
            if(result.Succeeded)
            {
                TempData["save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _db.LoginData.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Login user)
        {
            var userInfo = _db.LoginData.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }

            _db.LoginData.Remove(userInfo);
            int rowAffected = _db.SaveChanges();
            if(rowAffected > 0)
            {
                TempData["save"] = "User has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);

        }


    }
}
