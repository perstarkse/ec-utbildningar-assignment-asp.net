﻿using System.ComponentModel.DataAnnotations.Schema;
using WebApp.ViewModels;

namespace WebApp.Models.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? CategoryId { get ; set; }

        public string? ImageUrl { get; set; }
        public CategoryEntity? Category { get; set; }

        public static implicit operator ProductModel(ProductEntity productEntity)
        {
            return new ProductModel
            {
                Id= productEntity?.Id,
                Name = productEntity?.Name,
                Description = productEntity?.Description,
                Price = productEntity?.Price,
                Category = productEntity?.Category?.Name,
                ImageUrl = productEntity?.ImageUrl
            };
        }
    }
}
