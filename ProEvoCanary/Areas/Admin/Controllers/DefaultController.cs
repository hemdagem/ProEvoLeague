﻿using System.Web.Mvc;
using ProEvoCanary.Authentication;
using ProEvoCanary.Models;

namespace ProEvoCanary.Areas.Admin.Controllers
{
    [AccessAuthorize(UserType.Admin)]
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}