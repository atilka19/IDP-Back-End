using System;
using System.Collections.Generic;
using System.Linq;
using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDP_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _repository = categoryRepository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _repository.GetCategoryNames();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult<TaskCategory> Post([FromBody] string title)
        {
           return _repository.CreateNewCategory(title);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _repository.UpdateCategoryName(value, id);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.DeleteCategory(id);
        }
    }
}
