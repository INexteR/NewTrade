using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace NewTrade
{
    internal class ProductCommands : MarkupExtension
    {
        public static RoutedUICommand Add { get; } = new("Добавление товара.", "AddProduct", typeof(ProductCommands));
        public static RoutedUICommand Update { get; } = new("Редактирование товара.", "UpdateProduct", typeof(ProductCommands));
        public static RoutedUICommand Remove { get; } = new("Удаление товара.", "RemoveProduct", typeof(ProductCommands));

        public CommandEnum Mode { get; set; }

        public ProductCommands()
        { }

        public ProductCommands(CommandEnum mode)
        {
            Mode = mode;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Mode switch
            {
                CommandEnum.Add => Add,
                CommandEnum.Update => Update,
                CommandEnum.Remove => Remove,
                _ => throw new NotImplementedException()
            };
        }

        internal enum CommandEnum
        {
            Add, Update, Remove
        }

    }
}
