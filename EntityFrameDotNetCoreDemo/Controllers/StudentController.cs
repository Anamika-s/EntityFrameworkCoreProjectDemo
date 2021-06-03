using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameDotNetCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameDotNetCoreDemo.Controllers
{
    public class StudentController : Controller
    {
        StudentDbContext db;
        public StudentController(StudentDbContext studentDbContext)
        {
            db = studentDbContext;
        }
        public IActionResult Index()
        {
            if(TempData["msg"] != null)
            ViewBag.msg = TempData["msg"].ToString();
            return View(db.Students.ToList());
        }

        public IActionResult Create()
        {
            Student student = new Student();
            return View(student);

        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student != null)
            {
                db.Students.Add(student);
                db.SaveChanges();
                //ViewData["msg"] = "Record Inserted";

                TempData["msg"] = "Record Inserted";
            }
            //return View();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Student student = db.Students.FirstOrDefault(x => x.Id == id);
             if(student!=null)
                {   return View(student);
                }
             else
            {
                ViewBag.msg = "Record not found";
                return View();
            }
            }


        [HttpPost]
        public IActionResult Delete(int id, Student student)
        {
            db.Students.Remove(student);
            db.SaveChanges();
            TempData["msg"] = "Record deleted";
            return RedirectToAction("Index");
        
        }


        public IActionResult Display(int id)
        {
            Student student = db.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                return View(student);
            }
            else
            {
                ViewBag.msg = "Record not found";
                return View();
            }
        }


        public IActionResult Edit(int id)
    {
        Student student = db.Students.FirstOrDefault(x => x.Id == id);
        if (student != null)
        {
            return View(student);
        }
        else
        {
            ViewBag.msg = "Record not found";
            return View();
        }
    }


    [HttpPost]
    public IActionResult Edit(int id, Student student)
    {
            foreach(Student temp in db.Students)
            {
                if(temp.Id == id)
                {
                    temp.Name = student.Name;
                    temp.Address = student.Address;
                    temp.Marks = student.Marks;
                    break;
                }
               }
            db.SaveChanges();

            TempData["msg"] = "Record updated";
        return RedirectToAction("Index");

    }
}



    }




 
