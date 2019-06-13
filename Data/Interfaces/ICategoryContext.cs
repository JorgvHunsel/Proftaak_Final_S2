using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface ICategoryContext
    {
        List<Category> GetAllCategories();
    }
}
