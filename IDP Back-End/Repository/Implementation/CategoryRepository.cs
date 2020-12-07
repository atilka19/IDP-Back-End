using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DBContext _ctx;

        public CategoryRepository(DBContext ctx)
        {
            _ctx = ctx;
        }

        public TaskCategory CreateNewCategory(string name)
        {
            var newCategory = new TaskCategory();
            newCategory.Title = name;

            _ctx.Attach(newCategory).State = EntityState.Added;
            _ctx.SaveChanges();
            return newCategory;
        }

        public void DeleteCategory(int ID)
        {
            _ctx.Remove(_ctx.Categories.FirstOrDefault(c => c.ID == ID));
            _ctx.SaveChanges();
        }

        public TaskCategory GetCategoryByID(int ID)
        {
            var auc = _ctx.Categories
                      .Include(c => c.Tasks)
                      .FirstOrDefault(t => t.ID == ID);
            return auc;
        }

        public List<string> GetCategoryNames()
        {
            var categories = _ctx.Categories.ToList();
            var resList = new List<string>();

            foreach(TaskCategory cat in categories)
            {
                resList.Add(cat.Title);
            }

            return resList;
        }

        public void UpdateCategoryName(string newName, int ID)
        {
            var cat = _ctx.Categories
                      .FirstOrDefault(t => t.ID == ID);
            cat.Title = newName;

            _ctx.Attach(cat).State = EntityState.Modified;
            _ctx.SaveChanges();
        }
    }
}
