using Courier_Management_System.Data;
using Courier_Management_System.Models;
using Courier_Management_System.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courier_Management_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly CourierDbContext courierDbContext;
        public AdminController(CourierDbContext courierDbContext) 
        {
            this.courierDbContext = courierDbContext;
        }

        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }
      
        [HttpPost]
        public async Task<IActionResult> AdminRegister(AddAdminModel addAdminModel)
        {
                var admin = new Admin
                {
                    AdminEmail = addAdminModel.AdminEmail,
                    AdminPassword = addAdminModel.AdminPassword,
                    AdminName = addAdminModel.AdminName
                };

               await courierDbContext.Admins.AddAsync(admin);
               await courierDbContext.SaveChangesAsync();

                return RedirectToAction("AdminRegConfirmation", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AdminsList()
        {
            var adminsList = await courierDbContext.Admins.ToListAsync();
            return View(adminsList);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAdmin(AddAdminModel adminLoginModel)
        {
            var admin = await courierDbContext.Admins.FirstOrDefaultAsync(a => a.AdminEmail == adminLoginModel.AdminEmail);

            if (admin != null)
            {
                courierDbContext.Admins.Remove(admin);
                await courierDbContext.SaveChangesAsync();
                return RedirectToAction("AdminsList");
            }

            return RedirectToAction("AdminsList");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var admin = await courierDbContext.Admins.FirstOrDefaultAsync(a => a.AdminEmail == loginModel.AdminEmail && a.AdminPassword == loginModel.AdminPassword);

                if (admin != null)
                {
                    return RedirectToAction("Index", "Home"); //Admin panel redirect
                }
                else
                {
                    return RedirectToAction("Dashboard", "Home"); //dashboard redirect 
                }
            }
            return View(loginModel);
        }
    }
}
