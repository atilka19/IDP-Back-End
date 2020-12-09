﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDP_Back_End.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP_Back_End.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _repo;

        public TasksController(ITaskRepository taskRepository)
        {
            _repo = taskRepository;
        }

        // GET: TasksController
        public IActionResult Index()
        {
            return View();
        }

        // GET: TasksController/Details/5

        public IActionResult TaskById(int taskID)
        {
            var task = _repo.GetTaskByID(taskID);
            return View("PartialViews/_SingleTaskView", task);
        }

        // GET: TasksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TasksController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TasksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TasksController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TasksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TasksController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
