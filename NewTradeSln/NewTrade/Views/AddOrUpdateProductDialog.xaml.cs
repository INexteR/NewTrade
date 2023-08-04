using Mapping;
using Model;
using ShopSQLite;
using ShopViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Input;
using ViewModels;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics.Contracts;
using System.Windows.Media.Imaging;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateDialog.xaml
    /// </summary>
    internal partial class AddOrUpdateProductDialog : Window
    {
        private readonly AddOrUpdateProductDialogData dialogData;
        private string? ImagePath { get => dialogData.Product.Path; set => dialogData.Product.Path = value; }

        private AddOrUpdateProductDialog(DialogMode mode, TempProduct product, IProductsViewModel viewModel)
        {
            InitializeComponent();

            dialogData = (AddOrUpdateProductDialogData)Resources[nameof(dialogData)];
            dialogData.Mode = mode;
            dialogData.Product = product;
            dialogData.ProductsViewModel = viewModel;
            firstPath = product.Path;
            remove.Command = new RelayCommand(RemoveImageExecute, CanRemoveImageExecute);
        }

        public static void Update(IProduct product, IProductsViewModel viewModel)
        {
            TempProduct copy = product?.Create<TempProduct>()
                ?? throw new ArgumentNullException(nameof(product));

            var window = new AddOrUpdateProductDialog(DialogMode.Update, copy, viewModel)
            {
                Title = "Редактирование товара",
            };
            window.button.Command = viewModel.UpdateProduct;
            window.ShowDialog();
        }
        public static void Add(IProduct? product, IProductsViewModel viewModel)
        {
            TempProduct copy = product is null ? new TempProduct()
                                             : product.Create<TempProduct>();
            copy.Path = null; //при копировании сбрасываем изображение, у каждого товара должно быть свое
            var window = new AddOrUpdateProductDialog(DialogMode.Add, copy, viewModel) 
            { 
                Title = "Добавление товара"
            };
            window.button.Command = viewModel.AddProduct;
            window.ShowDialog();
        }

        #region Код, препятствующий неверному применению изображений
        private readonly string? firstPath;

        private void OnChooseImageClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Выбор изображения",
                Multiselect = false,
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.tif|Все файлы|*.*"
            };
            if (dialog.ShowDialog() is true)
            {
                using (var imageStream = File.OpenRead(dialog.FileName))
                {
                    var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile,
                        BitmapCacheOption.Default);
                    var frame = decoder.Frames[0];
                    if (frame.PixelWidth > 300 || frame.PixelHeight > 200)
                    {
                        "Разрешение изображения превышает 300×200 пикселей".Msg("Не удалось выбрать изображение");
                        return;
                    }
                }

                var fileName = Path.GetFileName(dialog.FileName);
                var newImage = Path.Combine(ImageNameToPathConverter.ImageFolderPath, fileName);                
                

                if (File.Exists(newImage))
                {   //если выбран файл, отличный от выбранного до него
                    if (fileName != ImagePath)
                    {   //если файл не равен начальному
                        if (fileName != firstPath)
                        {
                            "Данное изображение уже используется".Msg();
                            return;
                        }//если предыдущее значение файла не равно начальному и не пусто
                        if (ImagePath != firstPath && ImagePath != null)
                            File.Delete(Path.Combine(ImageNameToPathConverter.ImageFolderPath, ImagePath));
                    }                    
                }
                else
                {   //если файла не существует
                    File.Copy(dialog.FileName, newImage);
                    //если предыдущее значение файла не равно начальному и не пусто
                    if (ImagePath != firstPath && ImagePath != null)
                        File.Delete(Path.Combine(ImageNameToPathConverter.ImageFolderPath, ImagePath));                  
                }
                ImagePath = fileName;
            }
        }

        private void RemoveImageExecute()
        {
            var path = Path.Combine(ImageNameToPathConverter.ImageFolderPath, ImagePath!);
            if (ImagePath != firstPath)
                File.Delete(path);
            ImagePath = null;
        }

        private bool CanRemoveImageExecute()
        {
            return !string.IsNullOrWhiteSpace(ImagePath);
        }

        private bool saveFlag;

        private void OnSavingClick(object sender, RoutedEventArgs e)
        {
            //если файл не равен начальному и начальный не пуст
            if (ImagePath != firstPath && firstPath != null)
                File.Delete(Path.Combine(ImageNameToPathConverter.ImageFolderPath, firstPath));
            saveFlag = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //если не кнопка сохранения и предыдущее значение файла не равно начальному и не пусто
            if (!saveFlag && ImagePath != firstPath && ImagePath != null)
            {
                File.Delete(Path.Combine(ImageNameToPathConverter.ImageFolderPath, ImagePath));
            }
        }
        #endregion
    }

    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateProductDialogData : ViewModelBase
    {
        public DialogMode Mode { get => Get<DialogMode>(); set => Set(value); }
        public TempProduct Product { get => Get<TempProduct>()!; set => Set(value); }
        public IProductsViewModel ProductsViewModel { get => Get<IProductsViewModel>()!; set => Set(value); }
    }
}
