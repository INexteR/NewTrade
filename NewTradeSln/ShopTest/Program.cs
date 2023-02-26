// See https://aka.ms/new-console-template for more information
using ShopSQLite;

Console.WriteLine("Hello, World!");
var shop = new Shop(true);
var prds = shop.GetProducts();
Console.WriteLine();
