using System.Collections.Generic;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class CategoryLogic
    {
        private readonly ICategoryContext _category;

        public CategoryLogic(ICategoryContext category)
        {
            _category = category;
        }

        public List<Category> GetAllCategories()
        {
            return _category.GetAllCategories();
        }

       
    }
}
