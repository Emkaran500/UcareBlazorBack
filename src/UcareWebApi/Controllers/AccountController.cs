using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UcareApp.Auth.DTOs;
using UcareApp.Auth.Models;
using UcareApp.Options;

namespace UcareApp.Controllers;

public class AccountController : Controller
{
    private readonly JwtOptions jwtOptions;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(IOptionsSnapshot<JwtOptions> jwtOptionsSnapshot, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        this.jwtOptions = jwtOptionsSnapshot.Value;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                PhoneNumber = dto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                if ((await _roleManager.RoleExistsAsync("user")) == false)
                {
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }
                await _userManager.AddToRoleAsync(user, "user");

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(dto);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (ModelState.IsValid)
        {
            var foundUser = await _userManager.FindByEmailAsync(dto.Email);

            var result = await this._signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);
            if(result.IsLockedOut) {
                return base.BadRequest("User locked");
            }

            if(result.Succeeded == false) {
                return base.BadRequest("Incorrect Login or Password");
            }

            var roles = await _userManager.GetRolesAsync(foundUser);

            var claims = roles
            .Select(roleStr => new Claim(ClaimTypes.Role, roleStr))
            .Append(new Claim(ClaimTypes.NameIdentifier, foundUser.Id))
            .Append(new Claim(ClaimTypes.Email, foundUser.Email ?? "not set"))
            .Append(new Claim(ClaimTypes.Name, foundUser.UserName ?? "not set"));

            var signingKey = new SymmetricSecurityKey(jwtOptions.KeyInBytes);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(jwtOptions.LifeTimeInHours),
            signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var tokenStr = handler.WriteToken(token);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Invalid login attempt.");

        return View(dto);
    }

    [HttpGet("[controller]/[action]")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
