using Company.app.Models;
using Company.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.app.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleController> _logger;

        public RoleController
            (
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<RoleController> logger
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(RoleViewModel roleModel) 
        {
            var role = new IdentityRole
            {
                Name = roleModel.Name,
            }; 

            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View(roleModel);
        }

        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

          
            var UserViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(viewName, UserViewModel);

            

        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, RoleViewModel RoleModel)
        {

            if (id != RoleModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role is null)
                        return NotFound();

                    role.Name = RoleModel.Name;
                    role.NormalizedName = RoleModel.Name.ToUpper();
                    var result = await _roleManager.UpdateAsync(role);


                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Role Updated Successfully");
                        RedirectToAction(nameof(Index));


                    }


                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                }   
            }

            return View(RoleModel);

        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);

            if ((result.Succeeded))
                return RedirectToAction(nameof(Index));

            foreach (var item in result.Errors)
                _logger.LogError(item.Description);


            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            ViewBag.RoleId = roleId;


            var users = await _userManager.Users.ToListAsync();
            var UsersInRole = new List<UserInRoleViewModel>();


            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else userInRole.IsSelected = false;

                UsersInRole.Add(userInRole);
            }
            return View(UsersInRole);


        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var AppUser = await _userManager.FindByIdAsync(user.UserId);
                    if (AppUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(AppUser, role.Name))
                            await _userManager.AddToRoleAsync(AppUser, role.Name);

                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(AppUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(AppUser, role.Name);
                    }
                }

                return RedirectToAction("Update", new { id = roleId });

            }

            return View(users);

        }




    }
}


