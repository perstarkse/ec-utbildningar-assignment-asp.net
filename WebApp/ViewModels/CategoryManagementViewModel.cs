using WebApp.Models.Entities;

namespace WebApp.ViewModels
{
    public class CategoryManagementViewModel
    {
        public List<CategoryEntity> Categories { get; set; }
        public string? NewCategoryName { get; set; }

        public CategoryManagementViewModel()
        {
            Categories = new List<CategoryEntity>();
        }
    }

}
