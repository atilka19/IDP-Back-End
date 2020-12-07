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

        // Dependency Injection
        public CommentRepository(DBContext ctx)
        {
            _ctx = ctx;
        }

        public void CreateCommentAddToTask(int taskID, string text, string userName)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                throw new InvalidDataException("User does not exist!");
            }

            var task = _ctx.Tasks.FirstOrDefault(t => t.ID == taskID);
            if (task == null)
            {
                throw new InvalidDataException("Requested Task does not exist!");
            }
            task.Comments.Add(new Comment() {Text = text, User = user, TimePosted = new DateTime()});

            _ctx.Attach(task).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void DeleteComment(int ID)
        {
            _ctx.Remove(_ctx.Comments.FirstOrDefault(c => c.ID == ID));
            _ctx.SaveChanges();
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
