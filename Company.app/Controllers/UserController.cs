using company.services.Interfaces.Department.Dto;
using Company.app.Models;
using Company.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.app.Controllers
{
    [Authorize(Roles ="Admin")]

    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserController
            (
            UserManager<ApplicationUser> userManager,
            ILogger<UserController> logger
            )

        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task <IActionResult> Index(string SearchInput)
        {
            List<ApplicationUser> Users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                Users = await _userManager.Users.ToListAsync();
            }
            else
            {
                Users = await _userManager.Users.Where
                    (user => user.NormalizedEmail.Trim().Contains(SearchInput.Trim().ToUpper()))
                    .ToListAsync();
            }

            return View(Users);
        }

        public async Task <IActionResult> Details(string? id, string viewName = "Details")
        {
            var user = await _userManager.FindByIdAsync(id );
            if (user is null)
                return NotFound();

            if(viewName == "Update")
            {
                var userViewModel = new UserUpdateViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                };
                return View(viewName, userViewModel);

            }


            return View(viewName, user);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserUpdateViewModel applicationUser)
        {

            if (id != applicationUser.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                        return NotFound();

                    user.FirstName = applicationUser.FirstName;
                    var result = await _userManager.UpdateAsync(user);


                    if (result.Succeeded)
                    {
                        RedirectToAction(nameof(Index));
                        _logger.LogInformation("User Updated Successfully");

                    }


                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);
                    
                }
                catch (Exception ex) 
                {
                    _logger.LogError(ex.Message); 

                }
            }

            return View(applicationUser);

        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if ((result.Succeeded))
                return RedirectToAction(nameof(Index));

            foreach (var item in result.Errors)
                _logger.LogError(item.Description);


            return RedirectToAction(nameof(Index));

        }



    }
}
