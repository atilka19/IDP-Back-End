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
    public class CommentRepository : ICommentRepository
    {
        private readonly DBContext _ctx;
        private readonly ITaskRepository _taskRepo;

        // Dependency Injection
        public CommentRepository(DBContext ctx, ITaskRepository taskRepo)
        {
            _ctx = ctx;
            _taskRepo = taskRepo;
        }

        public Models.Task CreateCommentAddToTask(int taskID, string text, string userName)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                throw new InvalidDataException("User does not exist!");
            }

            var task = _taskRepo.GetTaskByID(taskID);
            if (task == null)
            {
                throw new InvalidDataException("Requested Task does not exist!");
            }
            task.Comments.Add(new Comment() {Text = text, User = user, TimePosted = DateTime.Now});

            _ctx.Attach(task).State = EntityState.Modified;
            _ctx.SaveChanges();

            return task;
        }

        public Models.Task DeleteComment(int ID)
        {
            var comment = _ctx.Comments.FirstOrDefault(c => c.ID == ID);
            _ctx.Remove(comment);
            _ctx.SaveChanges();

            return _taskRepo.GetTaskByID(comment.TaskID);
        }

        public void UpdateCommentText(int ID, string newText)
        {
            var comment = _ctx.Comments.FirstOrDefault(c => c.ID == ID);
            if(comment == null)
            {
                throw new InvalidDataException("Comment was not found");
            }

            comment.Text = newText;
            _ctx.Attach(comment).State = EntityState.Modified;
            _ctx.SaveChanges();
        }
    }
}
