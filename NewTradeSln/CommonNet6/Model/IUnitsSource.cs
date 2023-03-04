using System.Collections.Generic;

namespace Model
{
    public interface IUnitsSource
    {
        IReadOnlyList<IUnit> GetUnits();
    }
}
