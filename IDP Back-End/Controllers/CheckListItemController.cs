using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDP_Back_End.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP_Back_End.Controllers
{
    public class CheckListItemController : Controller
    {
        private readonly ICheckItemRepository _repo;

        public CheckListItemController(ICheckItemRepository repo)
        {
            _repo = repo;
        }

        // GET: CheckListItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: CheckListItemController/Delete/5
        public ActionResult Delete(int id)
        {
            var task = _repo.DeleteCheckListItem(id);
            return View("PartialViews/_SingleTaskView", task);
        }
    }
}
