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

        public void AddNewTask(string title, string createdBy, string categoryName)
        {
            if(title.Length == 0 || createdBy == null || categoryName == null)
            {
                throw new InvalidDataException("A value was null! Try again.");
            }
            var user = _ctx.Users.FirstOrDefault(u => u.UserName == createdBy);
            if(user == null)
            {
                throw new InvalidDataException("User was not found! Please log in again!");
            }

            var category = _ctx.Categories.FirstOrDefault(c => c.Title == categoryName);
            if (category == null)
            {
                throw new InvalidDataException("Category was not found!");
            }

            var newTask = new Task();
            newTask.Title = title;
            newTask.CreatedBy = user;
            newTask.CreatedByID = user.Id;
            newTask.Category = category;

            _ctx.Attach(newTask).State = EntityState.Added;
            _ctx.SaveChanges();
        }

        public Task AddUserToTask(int id, string taskOf)
        {
            var task = _ctx.Tasks.FirstOrDefault(t => t.ID == id);
            var user = _ctx.Users.FirstOrDefault(u => u.UserName == taskOf);

            task.TaskOf = user;

            _ctx.Attach(task).State = EntityState.Modified;
            _ctx.SaveChanges();

            return task;
        }

        public void DeleteTask(int ID)
        {
            _ctx.Remove(_ctx.Categories.FirstOrDefault(c => c.ID == ID));
            _ctx.SaveChanges();
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

        public Task UpdateTaskCategory(int taskID, string category)
        {
            var taskToUpdate = GetTaskByID(taskID);
            var newCategory = _ctx.Categories.FirstOrDefault(c => c.Title == category);

            if(taskToUpdate != null && newCategory != null)
            {
                taskToUpdate.Category = newCategory;
                _ctx.Attach(taskToUpdate).State = EntityState.Modified;
                _ctx.SaveChanges();

                return GetTaskByID(taskID);
            } else
            {
                throw new InvalidDataException("Task or Category was not found");
            }
        }
    }
}
