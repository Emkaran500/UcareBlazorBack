﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UcareApp.Auth.DTOs;
using UcareApp.Auth.Models;

namespace UcareApp.Controllers;

public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
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
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return Ok(ModelState);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (ModelState.IsValid)
        {
            var result = await this._signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return base.Ok();
            }

            ModelState.AddModelError("", "Invalid login attempt.");
        }

        return BadRequest(ModelState);
    }

    [HttpGet("[controller]/[action]")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return base.NoContent();
    }
}
