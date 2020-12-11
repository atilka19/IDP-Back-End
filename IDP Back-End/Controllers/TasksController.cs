﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP_Back_End.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ICommentRepository _commentRepo;
        private readonly ICheckItemRepository _checkListRepo;

        public TasksController(ITaskRepository taskRepository, ICommentRepository commentRepository, ICheckItemRepository checkListRepo)
        {
            _taskRepo = taskRepository;
            _commentRepo = commentRepository;
            _checkListRepo = checkListRepo;
        }

        // GET: TasksController
        public IActionResult Index()
        {
            return View();
        }

        // GET: TasksController/Details/5

        public IActionResult TaskById(int taskID)
        {
            var task = _taskRepo.GetTaskByID(taskID);
            return View("PartialViews/_SingleTaskView", task);
        }

        // GET: TasksController/Create
        [HttpPost]
        [Route("api/addTask")]
        public IActionResult AddTask([FromBody] TaskInputModel input)
        {
            var task = _taskRepo.AddNewTask(input.Title, input.Username, input.CategoryName);
            return View("PartialViews/_SingleTaskView", task);
        }

        // GET: TasksController/Create
        public IActionResult AddUserToTask(int taskId, string username)
        {
            var task = _taskRepo.AddUserToTask(taskId, username);
            return View("PartialViews/_SingleTaskView", task);
        }

        // GET: TasksController/Create
        public IActionResult AddCommentToTask(int taskId, string text, string username)
        {
            var task = _commentRepo.CreateCommentAddToTask(taskId, text, username);
            return View("PartialViews/_SingleTaskView", task);
        }

        // GET: TasksController/Create
        public IActionResult AddCheckListToTask(int taskId, string text)
        {
            var task = _checkListRepo.CreateNewListItemAddToTask(taskId, text);
            return View("PartialViews/_SingleTaskView", task);
        }

        // GET: TasksController/Edit/5
        public RedirectToActionResult EditTask(int ID, string newTitle, string newDescription, bool done)
        {
            _taskRepo.UpdateTask(ID, newTitle, newDescription, done);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: TasksController/Delete/5
        public RedirectToActionResult Delete(int id)
        {
            _taskRepo.DeleteTask(id);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
