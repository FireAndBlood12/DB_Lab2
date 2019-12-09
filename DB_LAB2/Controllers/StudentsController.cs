using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DB_LAB2.Database.Entities;
using DB_LAB2.Models;
using DB_LAB2.Database;
using DB_LAB2.Models.Shared;
using DB_LAB2.Models.Students;

namespace DB_LAB2.Controllers
{
    public class StudentsController : Controller
    {
        private DAO<Student> studentDAO;
        private DAO<Group> groupDAO;

        public StudentsController(DB_LAB2Context context)
        {
            studentDAO = new StudentDAO(DBConnection.getInstance());
            groupDAO = new GroupDAO(DBConnection.getInstance());
        }

        // GET: Students
        public IActionResult Index(int page = 1)
        {
            PageViewModel pageViewModel = new PageViewModel(studentDAO.Count(), page, 10);
            StudentsViewModel model = new StudentsViewModel(studentDAO.GetList(page), pageViewModel);
            return View(model);
        }

        // GET: Students/Details/5
        public IActionResult Details(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var student = studentDAO.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(groupDAO.GetList(0), "Id", "Code");
            return View(new CreateViewModel());
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Birthday,GroupId")] Student student)
        {
            if (ModelState.IsValid)
            {
                studentDAO.Create(student);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(groupDAO.GetList(0), "Id", "Code");
            return View(new CreateViewModel(student));
        }

        // GET: Students/Edit/5
        public IActionResult Edit(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var student = studentDAO.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(groupDAO.GetList(0), "Id", "Code");
            return View(new EditViewModel(student));
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,FirstName,LastName,Birthday,GroupId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    studentDAO.Update(student);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(groupDAO.GetList(0), "Id", "Code");
            return View(new EditViewModel(student));
        }

        // GET: Students/Delete/5
        public IActionResult Delete(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var student = studentDAO.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            studentDAO.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(long id)
        {
            return studentDAO.GetById(id) != null;
        }
    }
}
