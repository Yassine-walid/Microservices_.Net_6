using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new ();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.LoginAsync(obj);
            

            if (responseDto != null && responseDto.IsSuccess == true)
            {
               LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                //TempData["error"] = responseDto.Message;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", responseDto.Message);
                return View(obj);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticDetails.RoleCostumer,Value=StaticDetails.RoleCostumer},
                new SelectListItem{Text = StaticDetails.RoleAdmin, Value=StaticDetails.RoleAdmin},
            };

            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assignRole;

            if (result != null && result.IsSuccess == true) 
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = StaticDetails.RoleCostumer;
                }
                assignRole = await _authService.AssignRoleAsync(obj);
                if (assignRole != null && assignRole.IsSuccess == true)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            TempData["error"] = result.Message;
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticDetails.RoleCostumer,Value=StaticDetails.RoleCostumer},
                new SelectListItem{Text = StaticDetails.RoleAdmin, Value=StaticDetails.RoleAdmin},
            };

            ViewBag.RoleList = roleList;

            return View(obj);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
