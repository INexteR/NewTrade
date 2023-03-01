namespace Model
{
    public interface IOrderProduct
    {
        IOrder Order { get; }
        IProduct Product { get; }
        int ProductCount { get; }
    }
}
