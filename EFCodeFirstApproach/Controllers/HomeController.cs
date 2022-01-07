using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFCodeFirstApproach.Models;
using System.Data.Entity;

namespace EFCodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        StudentContext db = new StudentContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            if(ModelState.IsValid == true)
            {
                db.Students.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.InsertMessage = "<script>alert('Data Inserted !!')</script>";
                    //TempData["InsertMessage"] = "<script>alert('Data Inserted !!')</script>";
                    TempData["InsertMessage"] = "Data Inserted !!";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Data Not Inserted !!')</script>";
                }
            }
            
            return View();
        }

        public ActionResult Edit(int id)
        {
            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(row);
        }


        [HttpPost]
        public ActionResult Edit(Student s)
        {
            if(ModelState.IsValid == true)
            {
                db.Entry(s).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.UpdateMessage = "<script>alert('Data Updated !!')</script>";
                    //TempData["UpdateMessage"] = "<script>alert('Data Updated !!')</script>";
                    TempData["UpdateMessage"] = "Data Updated !!";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                    ViewBag.UpdateMessage = "<script>alert('Data Not Updated !!')</script>";
                }
            }
            
            return View();
        }
        public ActionResult Delete(int id)
        {
            if(id > 0)
            {
                var StudentIdRow = db.Students.Where(model => model.Id == id).FirstOrDefault();
                if(StudentIdRow != null)
                {
                    db.Entry(StudentIdRow).State = EntityState.Deleted;
                    int a = db.SaveChanges();
                    if(a > 0)
                    {
                        //TempData["DeleteMessage"] = "<script>alert('Data Deleted !!')</script>";
                        TempData["DeleteMessage"] = "Data Deleted !!";
                    }
                    else
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data Not Deleted !!')</script>";
                    }
                }
            }
            return RedirectToAction("Index");
            
            
        }

        public ActionResult Details(int id)
        {
            var DetailsById = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(DetailsById);
        }
     }
}