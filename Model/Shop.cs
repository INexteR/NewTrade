

namespace NewTrade.Model
{
    public class Shop
    {
        public string Name { get; } = "ООО «Ткани»";

        public Authorization Authorization { get; } = new();
    }
}
