using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLaboratory.Web.Models;
using OpenLaboratory.Web.ViewModels;

namespace OpenLaboratory.Web.Controllers
{
    [Authorize]
    public class RolesController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
        public async Task<IActionResult> Create(RoleCreateViewModel roleCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleCreateViewModel);
            }
            var role=new IdentityRole
            {
                Name=roleCreateViewModel.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty,error.Description);
            }

            return View(roleCreateViewModel);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var roleEditViewModel=new RoleEditViewModel
            {
                Id = id,
                RoleName = role.Name,
                Users = new Dictionary<string, string>()
            };

            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roleEditViewModel.Users.Add(user.UserName,user.StudentName);
                }
            }
            return View(roleEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleEditViewModel roleEditViewModel)
        {
            var role = await _roleManager.FindByIdAsync(roleEditViewModel.Id);
            if (role != null)
            {
                role.Name = roleEditViewModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty,"更新角色时出错");
                return View(roleEditViewModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "删除角色时出错");
            }
            ModelState.AddModelError(string.Empty, "没有找到该角色");
            return View("Index",await _roleManager.Roles.ToListAsync());
        }

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var vm=new UserRoleViewModel
            {
                RoleId = roleId,
            };
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user);
                }
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);
            if (user != null && role != null)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("Edit", new {id = role.Id});
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(userRoleViewModel);
            }
            ModelState.AddModelError(string.Empty, "用户或角色未找到");
            return View(userRoleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromRole(string roleId,string userName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && role != null)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Edit", new { id = role.Id });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return RedirectToAction("Edit", new { id = role.Id });
                }

                ModelState.AddModelError(string.Empty, "用户不在角色里");

                return RedirectToAction("Edit", new { id = role.Id });
            }

            ModelState.AddModelError(string.Empty, "用户或角色未找到");
            return RedirectToAction("Index",await _roleManager.Roles.ToListAsync());
        }
    }
}
