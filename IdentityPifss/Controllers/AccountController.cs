using IdentityPifss.Data;
using IdentityPifss.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityPifss.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        #region Configuration
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _SignInManeger;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManeger, RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _SignInManeger = signInManeger;
            _roleManager = roleManager;
        }

        #endregion

        #region Users
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Mobile,
                    City = model.City,
                    Gender=model.Gender

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Optionally, you can sign in the user after registration
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
        
                return View(model);
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_SignInManeger.IsSignedIn(User))
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async  Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                var result = await _SignInManeger.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
             
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }
            else
            {
                ModelState.AddModelError("", "Username and Password cannot be empty");
            }


            return View(model);
        }


        [HttpPost]
        public IActionResult Logout()
        {
            _SignInManeger.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Roles
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]

        public IActionResult CreateRole( )
        {
            return View();
        }
        [HttpPost]
  
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };

                var r = await _roleManager.CreateAsync(role);
                if (r.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
               foreach(var error in r.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(RolesList));
            }
            var RoleData = await _roleManager.FindByIdAsync(id);

            if (RoleData != null)
            {

                EditRoleViewModel role = new EditRoleViewModel
                {
                    RoleId = RoleData.Id,
                    RoleName = RoleData.Name,
                };
                return View(role);

            }
            return RedirectToAction(nameof(RolesList));
        }
          
        
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var RoleData = await _roleManager.FindByIdAsync(model.RoleId);
                if(RoleData == null) 
                { 
                    return RedirectToAction(nameof(RolesList)); 
                }
                RoleData.Name = model.RoleName;
                var r = await _roleManager.UpdateAsync(RoleData);
                if (r.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var error in r.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(RolesList));
            }
            var RoleData = await _roleManager.FindByIdAsync(id);

            if (RoleData != null)
            {

                EditRoleViewModel role = new EditRoleViewModel
                {
                    RoleId = RoleData.Id,
                    RoleName = RoleData.Name,
                };
                return View(role);

            }
            return RedirectToAction(nameof(RolesList));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(EditRoleViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var RoleData = await _roleManager.FindByIdAsync(model.RoleId);
                var r = await _roleManager.DeleteAsync(RoleData);
                if (r.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var error in r.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);
        }
        #endregion
    }
}
