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
using DB_LAB2.Models.Groups;
using DB_LAB2.Models.Shared;

namespace DB_LAB2.Controllers
{
    public class GroupsController : Controller
    {
        private DAO<Group> groupDAO;
        public GroupsController()
        {
            groupDAO = new GroupDAO(DBConnection.getInstance());
        }

        // GET: Groups
        public IActionResult Index(int page = 1)
        {
            PageViewModel pageViewModel = new PageViewModel(groupDAO.Count(), page, 10);
            GroupsViewModel model = new GroupsViewModel(groupDAO.GetList(page), pageViewModel);
            return View(model);
        }

        // GET: Groups/Details/5
        public IActionResult Details(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var @group = groupDAO.GetById(id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Code,Id,EntranceYear")] Group @group)
        {
            if (ModelState.IsValid)
            {
                groupDAO.Create(group);
                return RedirectToAction(nameof(Index));
            }
            return View(new CreateViewModel(group.Id, group.Code, group.EntranceYear));
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var @group = groupDAO.GetById(id);
            if (@group == null)
            {
                return NotFound();
            }
            EditViewModel editView = new EditViewModel(group);
            return View(editView);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Code,Id,EntranceYear")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    groupDAO.Update(group);
                }
                catch (Exception)
                {
                    if (!GroupExists(@group.Id))
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
            EditViewModel editView = new EditViewModel(group);
            return View(editView);
        }

        // GET: Groups/Delete/5
        public IActionResult Delete(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var @group = groupDAO.GetById(id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var @group = groupDAO.GetById(id);
            groupDAO.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(long id)
        {
            return groupDAO.GetById(id) != null;
        }
    }
}
