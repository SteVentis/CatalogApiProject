using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await _repository.GetItemsAsync())
                        .Select(item => item.AsDto());
            
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync),new { Id = item.Id }, item.AsDto());
        }

        //PUT/items/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with // With-expressions
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await _repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
             var existingItem = await _repository.GetItemAsync(id);

            if(existingItem is null)
            {
                return NotFound();
            }
            await _repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}