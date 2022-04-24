using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingProject.Models;

namespace TestingProject.Controllers
{
    public class StudentController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FgnL0MXRm2nMu78eYnTjursaRpfsT8iwjNEaf3p4",
            BasePath = "https://asp-mvc-with-android-c8825-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        // GET: Student
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Students");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Student>();
            foreach(var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Student>(((JProperty)item).Value.ToString()));
            }

            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                AddStudentToFirebase(student);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        private void AddStudentToFirebase(Student student)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = student;
            PushResponse response = client.Push("Students/", data);
            data.student_id = response.Result.name;
            SetResponse setResponse = client.Set("Students/" + data.student_id, data);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Students/" + id);
            Student data = JsonConvert.DeserializeObject<Student>(response.Body);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Students/" + id);
            Student data = JsonConvert.DeserializeObject<Student>(response.Body);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Students/" + student.student_id, student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Students/" + id);
            return RedirectToAction("Index");
        }
    }
} 