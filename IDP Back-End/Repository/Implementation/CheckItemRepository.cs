using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Implementation
{
    public class CheckItemRepository : ICheckItemRepository
    {
        private readonly DBContext _ctx;
        private readonly ITaskRepository _taskRepo;

        // Dependency Injection
        public CheckItemRepository(DBContext ctx, ITaskRepository taskRepo)
        {
            _ctx = ctx;
            _taskRepo = taskRepo;
        }

        public Models.Task CreateNewListItemAddToTask(int taskID, string text)
        {
            var task = _taskRepo.GetTaskByID(taskID);
            if (task == null)
            {
                throw new InvalidDataException("Requested Task does not exist!");
            }
            task.CheckListItems.Add(new CheckListItem() { Text = text, Done = false });

            _ctx.Attach(task).State = EntityState.Modified;
            _ctx.SaveChanges();

            return task;
        }

        public void CheckListItemDone(int ID)
        {
            var item = _ctx.CheckListItems.FirstOrDefault(c => c.ID == ID);
            if (item == null)
            {
                throw new InvalidDataException("Check List Item was not found");
            }

            if (item.Done == false)
            {item.Done = true;}
            else
            {item.Done = false;}

            _ctx.Attach(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void UpdateCheckListItemText(int ID, string newText)
        {
            var item = _ctx.CheckListItems.FirstOrDefault(c => c.ID == ID);
            if (item == null)
            {
                throw new InvalidDataException("Check List Item was not found");
            }

            item.Text = newText;

            _ctx.Attach(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void DeleteCheckListItem(int ID)
        {
            _ctx.Remove(_ctx.CheckListItems.FirstOrDefault(c => c.ID == ID));
            _ctx.SaveChanges();
        }
    }
}
