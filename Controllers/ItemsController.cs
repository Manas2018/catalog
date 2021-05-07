using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using catalog.Repositories;
using catalog.Entities;
using System;
using catalog.Dtos;
using System.Linq;
using System.Data.Common;

namespace catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepo itemsRepo;

        public ItemsController(IItemsRepo itemsRepo)
        {
            this.itemsRepo = itemsRepo;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = itemsRepo.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = itemsRepo.GetItem(id).AsDto();

            if(item is null){
                return NotFound();
            }
            
            return item;
        }

        [HttpPost]
        public ActionResult<Item> CreateItem(CreateItemDto itemDto){

            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };

            itemsRepo.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new{id = item.Id }, item.AsDto());
        }


        [HttpPut]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto){
            
            var existingItem = itemsRepo.GetItem(id);

            if(existingItem is null){
                return NotFound();
            }

            Item UpdatedItem = existingItem with{
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            
            itemsRepo.UpdateItem(UpdatedItem);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteItem(Guid id){
            
            var existingItem = itemsRepo.GetItem(id);

            if(existingItem is null){
                return NotFound();
            }

            itemsRepo.DeleteItem(id);
            
            return NoContent();
        }
        
    }
}