using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Interface
{
    public interface ICommentRepository
    {
        // CREATE
        Models.Task CreateCommentAddToTask(int taskID, string text, string userName);
        // DELETE
        Models.Task DeleteComment(int ID);

        // UPDATE
        void UpdateCommentText(int ID, string newText);
    }
}
