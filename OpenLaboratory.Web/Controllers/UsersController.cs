using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLaboratory.Web.Data;
using OpenLaboratory.Web.Models;
using OpenLaboratory.Web.ViewModels;

namespace OpenLaboratory.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //users的首页,展示所有user
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userCreateViewModel);
            }
            var user=new ApplicationUser
            {
                UserName = userCreateViewModel.UserName,
                StudentName = userCreateViewModel.StudentName
            };
            //创建用户和密码
            var result = await _userManager.CreateAsync(user, userCreateViewModel.Password);
            if (result.Succeeded)
            {
                //成功则重定向到index.更新用户列表
                return RedirectToAction("Index", await _userManager.Users.ToListAsync());
            }
            else
            {
                //否则输出所有错误以及明细,返回create
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(userCreateViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty,"删除用户时发生错误");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "用户找不到");
            }

            return View("Index", await _userManager.Users.ToListAsync());
        }
        
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var userEditViewModel = new UserEditViewModel()
            {
                UserName = user.UserName,
                StudentName = user.StudentName,
                Password = string.Empty
            };
            return View(userEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditViewModel userEditViewModel)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            
            user.UserName = userEditViewModel.UserName;
            user.StudentName = userEditViewModel.StudentName;
            if (userEditViewModel.Password != string.Empty)
            {
                //生成重置密码验证token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //重置密码
                await _userManager.ResetPasswordAsync(user, token, userEditViewModel.Password);
            }
            //验证修改结果
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "更新用户信息时发生错误");
            return View(userEditViewModel);
        }
    }
}
