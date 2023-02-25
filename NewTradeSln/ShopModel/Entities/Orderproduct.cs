﻿namespace ShopModel.Entities
{
    internal partial class Orderproduct
    {
        public int OrderId { get; set; }
        public string ProductArticleNumber { get; set; } = null!;
        public int ProductCount { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product ProductArticleNumberNavigation { get; set; } = null!;
    }
}
