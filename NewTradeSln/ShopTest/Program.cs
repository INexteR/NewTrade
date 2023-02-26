// See https://aka.ms/new-console-template for more information
using ShopSQLite;
using static System.Console;

WriteLine("Hello, World!");
var shop = new Shop(true);
var prds = shop.GetProducts();

WriteLine(prds.Count());
ReadLine();
