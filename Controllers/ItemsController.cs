using System;
using System.Collections.Generic;
using CatalogApiProject.Entities;
using CatalogApiProject.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace CatalogApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepo _repository; 
        public ItemsController()
        {
            _repository = new InMemItemsRepo();
        }

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return _repository.GetItems();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}