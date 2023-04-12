using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{

    private readonly AuthService _auth;

    public AccountController(AuthService auth)
    {
        _auth = auth;
    }


    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(UserSignUpViewModel model)
    {
        if (ModelState.IsValid)
        {

            if(await _auth.SignUpAsync(model))
                return RedirectToAction("Index");

            ModelState.AddModelError("", "A user with the same email-address already exists!"); 
        }
        
        return View(model);
    }
}
