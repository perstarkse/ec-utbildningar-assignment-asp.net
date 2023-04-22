﻿using WebApp.Models;
using WebApp.Models.Entities;

namespace WebApp.ViewModels
{
    public class ProductAdminViewModel
    {
        public List<ProductModel> Products { get; set; }
        public List<CategoryEntity> Categories { get; set; }
    }
}
