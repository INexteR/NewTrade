using System.Collections.Generic;

namespace Model
{
    public interface ICategoriesSource
    {
        IReadOnlyList<ICategory> GetCategories();
    }
}
