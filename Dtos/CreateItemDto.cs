using System.ComponentModel.DataAnnotations;

namespace CatalogApiProject.Dtos
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(1,100)]
        public decimal Price { get; init; }
    }
}