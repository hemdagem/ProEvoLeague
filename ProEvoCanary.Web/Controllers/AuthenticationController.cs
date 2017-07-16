﻿using System.Web.Mvc;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Models;
using UserType = ProEvoCanary.Domain.Authentication.UserType;

namespace ProEvoCanary.Web.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationHandler _authenticationHandler;

        public AuthenticationController(IUserRepository userRepository, IAuthenticationHandler authenticationHandler)
        {
            _userRepository = userRepository;
            _authenticationHandler = authenticationHandler;
        }

        // GET: Authentication/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            _userRepository.CreateUser(model.Username, model.Forename, model.Surname, model.EmailAddress, model.Password);
            return RedirectToAction("Index", "Default");
        }

        [AccessAuthorize(UserType.Admin)]
        public ActionResult AdminCreate()
        {
            return View("AdminCreate");
        }

        [AccessAuthorize(UserType.Admin)]
        // POST: Authentication/Create
        [HttpPost]
        public ActionResult AdminCreate(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.CreateUser(model.Username, model.Forename, model.Surname, model.EmailAddress, model.Password) > 0)
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            return View(model);
        }

        // GET: Authentication
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }

        // POST: Authentication
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            var userModel = _userRepository.Login(new Domain.Models.LoginModel(model.Username,model.Password));
            _authenticationHandler.SignIn(userModel.Forename, userModel.UserType.ToString(), userModel.UserId);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Default");
        }

        public ActionResult Signout()
        {
            _authenticationHandler.SignOut();
            return RedirectToAction("Index", "Default");
        }
    }
}
