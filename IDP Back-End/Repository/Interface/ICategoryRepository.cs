using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Interface
{
    public interface ICategoryRepository
    {
        // READ
        List<TaskCategory> GetAllCategorries();
        TaskCategory GetCategoryByID(int ID); 

        // CREATE
        TaskCategory CreateNewCategory(string name);

        // UPDATE
        void UpdateCategoryName(string newName, int ID);

        // DELETE
        void DeleteCategory(int ID);
    }
}
