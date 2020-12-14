using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IDP_Back_End.Repository.Interface
{
    public interface ITaskRepository
    {
        // READ
        Task GetTaskByID(int ID);
        // CREATE
        void AddNewTask(string title, string createdBy, string categoryName);
        Task AddUserToTask(int id, string taskOf);

        // UPDATE
        Task UpdateTaskCategory(int taskID, string category);
        void UpdateTask(int ID, string newTitle, string newDescription, bool done);

        // DELETE
        void DeleteTask(int ID);
    }
}
