using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDP_Back_End.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP_Back_End.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        // GET: CommentControllers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: CommentControllers/Delete/5
        public ActionResult Delete(int id)
        {
            var task = _commentRepo.DeleteComment(id);
            return View("PartialViews/_SingleTaskView", task);
        }
    }
}
