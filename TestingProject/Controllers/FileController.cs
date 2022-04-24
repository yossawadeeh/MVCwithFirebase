using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingProject.Models;

namespace TestingProject.Controllers
{
    public class FileController : Controller
    {
        private static string ApiKey = "YOUR_API_KEY";
        private static string Bucket = "asp-mvc-with-android-c8825.appspot.com";
        private static string AuthEmail = "your@email.com";
        private static string AuthPassword = "yourpasword";

        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Department department, HttpPostedFileBase file)
        {
            return View();
        }
    }
}