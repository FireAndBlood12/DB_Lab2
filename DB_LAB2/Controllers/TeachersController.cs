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
using DB_LAB2.Models.Teachers;

namespace DB_LAB2.Controllers
{
    public class TeachersController : Controller
    {
        private DAO<Subject> subjectDAO;
        private TeacherDAO teacherDAO;

        public TeachersController()
        {
            teacherDAO = new TeacherDAO(DBConnection.getInstance());
            subjectDAO = new SubjectDAO(DBConnection.getInstance());
        }

        // GET: Teachers
        public IActionResult Index(int page = 1)
        {
            PageViewModel pageViewModel = new PageViewModel(teacherDAO.Count(), page, 10);
            TeachersViewModel model = new TeachersViewModel(teacherDAO.GetList(page), pageViewModel);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TeachersViewModel teachersView)
        {
            PageViewModel pageViewModel = new PageViewModel(1, 1, 1);
            TeachersViewModel model = new TeachersViewModel(
                teacherDAO.Search(teachersView.MinExp, teachersView.MaxExp, teachersView.SubjectTitle), pageViewModel);
            return View(model);
        }

        // GET: Teachers/Details/5
        public IActionResult Details(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var teacher = teacherDAO.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            ViewData["MainSubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Experience,MainSubjectId")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacherDAO.Create(teacher);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainSubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title");
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public IActionResult Edit(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var teacher = teacherDAO.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["MainSubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title", teacher.MainSubjectId);
            return View(new EditViewModel(teacher));
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,FirstName,LastName,Experience,MainSubjectId")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    teacherDAO.Update(teacher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainSubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title", teacher.MainSubjectId);
            return View(new EditViewModel(teacher));
        }

        // GET: Teachers/Delete/5
        public IActionResult Delete(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var teacher = teacherDAO.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            teacherDAO.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(long id)
        {
            return teacherDAO.GetById(id) != null;
        }
    }
}
