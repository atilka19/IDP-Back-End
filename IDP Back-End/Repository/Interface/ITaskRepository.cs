using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IDP_Back_End.Repository.Interface
{
    public interface ITaskRepository
    {
        // READ
        List<Task> GetAllTasksInCategory(string Category);
        Task GetTaskByID(int ID);
        // CREATE
        Task CreateTask(string title, string description, int categoryID, string createdBy, string taskOf);

        // UPDATE
        void UpdateTaskCategory(int taskID, string category);
        void UpdateTask(int ID, string newTitle, string newDescription, bool done);

        // DELETE
        void DeleteTask(int ID);
    }
}
