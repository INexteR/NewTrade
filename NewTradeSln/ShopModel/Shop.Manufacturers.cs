using Interfaces;
using System.Collections.ObjectModel;

namespace ShopModel
{
    public partial class Shop : IManufacturersSource
    {
        public event EventHandler<ManufacturersChangedArgs> ManufacturersChanged;

        private readonly List<IManufacturer> manufacturerList = new List<IManufacturer>();
        private readonly ReadOnlyCollection<IManufacturer> manufacturers;
        public IReadOnlyCollection<IManufacturer> GetManufacturers()
            => manufacturers;

        protected virtual void OnManufacturerChanged(ManufacturersChangedArgs e)
        {
            // Если нужна какая-то логика зависимая от изменения коллекции производителей.
        }

        private void ResetManufacturers()
        {
            manufacturerList.Clear();
            manufacturerList.AddRange(manufacturerList);
            ManufacturersChanged(this, ManufacturersChangedArgs.Reset());
        }
    }
}
