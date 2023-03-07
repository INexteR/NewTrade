using NewTrade.Views;
using System;
using System.Windows.Markup;
using ViewModels;

namespace NewTrade
{
    internal class ProductCommands : MarkupExtension
    {
        public static RelayCommand Add { get; } = new(p =>
        {
            if (p is AddOrUpdateProductDialogArgs args)
                AddOrUpdateProductDialog.Add(args);
            else if (p is IProductsViewModel vm)
                AddOrUpdateProductDialog.Add(new AddOrUpdateProductDialogArgs(null, vm));
        });
        public static RelayCommand<AddOrUpdateProductDialogArgs> Update { get; } = new(args => AddOrUpdateProductDialog.Update(args));

        public DialogMode Mode { get; set; }

        public ProductCommands()
        { }

        public ProductCommands(DialogMode mode)
        {
            Mode = mode;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Mode switch
            {
                DialogMode.Add => Add,
                DialogMode.Update => Update,
                _ => throw new NotImplementedException()
            };
        }
    }
}
