using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoRSN.Models;

namespace AutoRSN.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ViewResult Index()
        {
            return View();
        }


        public ViewResult Index(Search oSearch)
        {
            return View("Results", oSearch);
        }
    }
}