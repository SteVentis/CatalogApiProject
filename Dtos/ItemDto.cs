using System;

namespace CatalogApiProject.Dtos
{
    public record ItemDto 
    {
        public Guid Id { get; init; } // Init: After the creation you can no longer modify these property(only for properties)
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }


    }
}