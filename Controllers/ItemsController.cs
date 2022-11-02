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

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem),new { Id = item.Id }, item.AsDto());
        }

        //PUT/items/id
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = _repository.GetItem(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with // With-expressions
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            _repository.UpdateItem(updatedItem);

            return NoContent();
        }
    }
}