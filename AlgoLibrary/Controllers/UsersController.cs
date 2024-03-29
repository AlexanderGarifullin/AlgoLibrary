﻿using Microsoft.AspNetCore.Mvc;
using AlgoLibrary.Models;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace AlgoLibrary.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Users(string msg)
        {
            string userRoleString = HttpContext.Session.GetString("UserRole");
            UserRole userRole = Enum.Parse<UserRole>(userRoleString);

            if (userRole == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            ViewData["ErrorMessage"] = msg;
            List<UserModel> users = _context.User.ToList();
            return View(users);
        }
        public IActionResult Delete(int id)
        {
            string userRoleString = HttpContext.Session.GetString("UserRole");
            UserRole userRole = Enum.Parse<UserRole>(userRoleString);

            if (userRole == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            if (id == HttpContext.Session.GetInt32("UserId"))  
            {
                return RedirectToAction("Users", new { msg = StringConstant.DeleteHimselfError});
            }
            var userToDelete = _context.User.Find(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.User.Remove(userToDelete);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        public IActionResult Create()
        {
            string userRoleString = HttpContext.Session.GetString("UserRole");
            UserRole userRole = Enum.Parse<UserRole>(userRoleString);

            if (userRole == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            return View("Change", new UserModel());
        }
        public IActionResult Edit(int id)
        {
            string userRoleString = HttpContext.Session.GetString("UserRole");
            UserRole userRole = Enum.Parse<UserRole>(userRoleString);

            if (userRole == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            UserModel user = _context.User.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View("Change", user);
        }

        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            string userRoleString = HttpContext.Session.GetString("UserRole");
            UserRole userRole = Enum.Parse<UserRole>(userRoleString);

            if (userRole == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            userModel.Password = userModel.Password.Trim();
            userModel.Login = userModel.Login.Trim();

            int id = userModel.UserId;
            string login = userModel.Login;
            string password = userModel.Password;
            string role = userModel.Role;

            if (!IsCorrectUserData(id, login, password, role))
            {
                ViewData["ErrorMessage"] = StringConstant.UsersInputError;
                if (id == 0) return View("Change", new UserModel()); 
                else
                {
                    return View("Change", userModel);
                }
            }
            if (id == 0)
            {
                // add
                try
                {
                    userModel.Password = Hashing.EncryptPassword(password);
                    _context.User.Add(userModel);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Users));
                }
                catch (DbUpdateException ex) when (ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
                {
                    ViewData["ErrorMessage"] = StringConstant.UsersDuplicateNameError + "\"" + login + "!";
                    return View("Change", new UserModel());
                }
                catch (Exception e)
                {
                    ViewData["ErrorMessage"] = StringConstant.DbError;
                    return View("Change", new UserModel());
                }              
            } else
            {
                // edit
                try
                {
                    if (id == HttpContext.Session.GetInt32("UserId")) RedirectToAction(nameof(Users));
                    var existingUser = _context.User.FirstOrDefault(u => u.UserId == id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }
                    existingUser.Login = login;
                    existingUser.Password = Hashing.EncryptPassword(password);
                    existingUser.Role = role;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Users));
                }
                catch (DbUpdateException ex) when (ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
                {
                    ViewData["ErrorMessage"] = StringConstant.UsersDuplicateNameError + "\"" + login + "!";
                    return View("Change", userModel);
                }
                catch (Exception e)
                {
                    ViewData["ErrorMessage"] = StringConstant.DbError;
                    return View("Change", new UserModel());
                }
            }
        }

        public bool IsCorrectUserData(int id, string login, string password, string role)
        {
            if (login.Length > 40) return false;
            if (password.Length > 256) return false;
            return true;
        }
    }
}
