using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.APIViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.WebApp.Controllers.API
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
            // GET: api/<controller>
            [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryManager.GetAll();
            return Ok(categories);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var category = _categoryManager.GetById(id);
            if (category == null)
            {
                return BadRequest("No Category Found");
            }
            return Ok(category);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Category model)
        {
            if (ModelState.IsValid)
            {
                var isSaved = _categoryManager.Add(model);
                if (isSaved)
                {
                    return Ok(model);
                }
            }

            return BadRequest(ModelState);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Category model)
        {
            var category = _categoryManager.GetById(id);
            if (category == null)
            {
               return BadRequest("No Product Found to Update!");
            }
            if (ModelState.IsValid)
            {

                category.Name = model.Name;
              

                bool isUpdated = _categoryManager.Update(category);
                if (isUpdated)
                {
                    return Ok(category);
                }
            }


            return BadRequest();
        }                                                    

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var category = _categoryManager.GetById(id);
            if (category == null)
            {
                //return BadRequest("No Product found to Delete");
            }

            var isRemoved = _categoryManager.Remove(category);
            if (isRemoved)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
