using System.Collections.Generic;

namespace Model
{
    public interface ISuppliersSource
    {
        IReadOnlyList<ISupplier> GetSuppliers();
    }
}
