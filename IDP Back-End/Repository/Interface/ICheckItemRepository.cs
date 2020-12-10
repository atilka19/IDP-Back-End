using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Interface
{
    public interface ICheckItemRepository
    {
        // CREATE
        Models.Task CreateNewListItemAddToTask(int taskID, string text);
        // UPDATE
        void CheckListItemDone(int ID);
        void UpdateCheckListItemText(int ID, string newText);
        // DELETE
        void DeleteCheckListItem(int ID);
    }
}
