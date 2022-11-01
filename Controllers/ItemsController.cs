using System;
using System.Collections.Generic;
using System.Linq;
using CatalogApiProject.Dtos;
using CatalogApiProject.Entities;
using CatalogApiProject.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace CatalogApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository; 
        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = _repository.GetItems().Select(item => item.AsDto());
            
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }
    }
}