using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NewTrade
{
    public class ProductsHelper
    {
        public static ReadOnlyDictionary<string, ListSortDirection?> Directions { get; }
            = new Dictionary<string, ListSortDirection?>
            {
                { "По умолчанию", null },
                { "По возрастанию", ListSortDirection.Ascending },
                { "По убыванию", ListSortDirection.Descending }
            }.AsReadOnly();//ну да, здесь нужен DictionaryConverter

        public static object AllManufacturers { get; } = new { Id = -1, Name = "Все производители" };
    }
}
