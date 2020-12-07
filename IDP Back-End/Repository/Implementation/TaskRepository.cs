using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IDP_Back_End.Repository.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DBContext _ctx;

        public TaskRepository(DBContext ctx)
        {
            _ctx = ctx;
        }

        public Task CreateTask(string title, string description, int categoryID, string createdBy, string taskOf)
        {
            var newTask = new Task();

            // EF relational stuff
            newTask.Category = _ctx.Categories.FirstOrDefault(t => t.ID == categoryID);
            newTask.TaskOf = _ctx.Users.FirstOrDefault(u => u.UserName == taskOf);
            newTask.CreatedBy = _ctx.Users.FirstOrDefault(u => u.UserName == createdBy);

            // Foreign Keys
            newTask.CategoryID = newTask.Category.ID;
            newTask.TaskOfID = newTask.TaskOf.Id;
            newTask.CreatedByID = newTask.CreatedBy.Id;

            // Other Data
            newTask.Title = title;
            newTask.Description = description;
            newTask.TimeCreated = new DateTime();
            newTask.Done = false;

            _ctx.Attach(newTask).State = EntityState.Added;
            _ctx.SaveChanges();
            return newTask;
        }

        public void DeleteTask(int ID)
        {
            _ctx.Remove(_ctx.Categories.FirstOrDefault(c => c.ID == ID));
            _ctx.SaveChanges();
        }

        public List<Task> GetAllTasksInCategory(string Category)
        {
            return _ctx.Tasks
                .Include(t => t.TaskOf)
                .Where(t => t.Category.Title == Category).ToList();
        }

        public Task GetTaskByID(int ID)
        {
            var task = _ctx.Tasks
                      .Include(t => t.CheckListItems)
                      .Include(t => t.Comments)
                      // Might not use the next 2, depending on Front-end implementation, will leave it here for now.
                      .Include(t => t.CreatedBy)
                      .Include(t => t.TaskOf)
                      .FirstOrDefault(t => t.ID == ID);
            return task;
        }

        public void UpdateTask(int ID, string newTitle, string newDescription, bool done)
        {
            var task = _ctx.Tasks.FirstOrDefault(t => t.ID == ID);
            if (task == null)
            {
                throw new InvalidDataException("Requested task does not exist!");
            }

            task.Title = newTitle;
            task.Description = newDescription;
            if (done != task.Done)
            {
                task.Done = done;
            }

            _ctx.Attach(task).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void UpdateTaskCategory(int taskID, string category)
        {
            var taskToUpdate = GetTaskByID(taskID);
            var newCategory = _ctx.Categories.FirstOrDefault(c => c.Title == category);

            if(taskToUpdate != null && newCategory != null)
            {
                taskToUpdate.Category = newCategory;
                _ctx.Attach(taskToUpdate).State = EntityState.Modified;
                _ctx.SaveChanges();
            } else
            {
                throw new InvalidDataException("Task or Category was not found");
            }
        }
    }
}
