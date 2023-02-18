
using ShopModel.Entities;

namespace ShopModel.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; }

        public string Name { get; set; }

        public CategoryDTO(Category category)
        {
            Id = category.IdCategory;
            Name = category.CategoryName;
        }
    }
}
