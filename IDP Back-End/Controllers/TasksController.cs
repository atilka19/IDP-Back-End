using System;
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
            return View("PartialViews/_SingleTaskView", _taskRepo.GetTaskByID(taskID));
        }

        // GET: TasksController/Create
        [HttpPost]
        [Route("api/addTask")]
        public ActionResult AddTask([FromBody] TaskInputModel input)
        {
            try
            {
                _taskRepo.AddNewTask(input.Title, input.Username, input.CategoryName);
                return StatusCode(200, Ok());
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
        [HttpPost]
        [Route("api/updateCheckListItem")]
        public IActionResult EditCheckListItem([FromBody] ItemInputModel item)
        {
            try
            {
                _checkListRepo.UpdateCheckListItem(item.Id, item.Text, item.Done);
                return StatusCode(200, Ok());

            } catch(Exception e) 
            {
                return StatusCode(400, e.Message);
            }
        }

        // GET: TasksController/Create
        public IActionResult AddUserToTask(int taskId, string username)
        {
            return View("PartialViews/_SingleTaskView", _taskRepo.AddUserToTask(taskId, username));
        }

        // GET: TasksController/Create
        public IActionResult AddCommentToTask(int taskId, string text, string username)
        {
            return View("PartialViews/_SingleTaskView", _commentRepo.CreateCommentAddToTask(taskId, text, username));
        }

        // GET: TasksController/Create
        public IActionResult AddCheckListToTask(int taskId, string text)
        {
            return View("PartialViews/_SingleTaskView", _checkListRepo.CreateNewListItemAddToTask(taskId, text));
        }

        // GET: TasksController/Create
        [HttpPost]
        [Route("api/changeTaskCategory")]
        public IActionResult ChangeTaskCategory([FromBody] UpdateCategoryModel model)
        {
            try
            {
                _taskRepo.UpdateTaskCategory(model.taskId, model.newCategoryName);
                return StatusCode(200, Ok());
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        // GET: TasksController/Edit/5
        public IActionResult EditTask(int ID, string newTitle, string newDescription, bool done)
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
