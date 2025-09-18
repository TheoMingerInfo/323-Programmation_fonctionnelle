using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {

        List<Product> products = new List<Product>
        {
            new Product { Location = 1, Producer = "Bornand", ProductName = "Pommes", Quantity = 20, Unit = "kg", PricePerUnit = 5.50 },
            new Product { Location = 1, Producer = "Bornand", ProductName = "Poires", Quantity = 16, Unit = "kg", PricePerUnit = 5.50 },
            new Product { Location = 1, Producer = "Bornand", ProductName = "Pastèques", Quantity = 14, Unit = "pièce", PricePerUnit = 5.50 },
            new Product { Location = 1, Producer = "Bornand", ProductName = "Melons", Quantity = 5, Unit = "kg", PricePerUnit = 5.50 },
            new Product { Location = 2, Producer = "Dumont", ProductName = "Noix", Quantity = 20, Unit = "sac", PricePerUnit = 5.50 },
            new Product { Location = 2, Producer = "Dumont", ProductName = "Raisin", Quantity = 6, Unit = "kg", PricePerUnit = 5.50 },
            new Product { Location = 2, Producer = "Dumont", ProductName = "Pruneaux", Quantity = 13, Unit = "kg", PricePerUnit = 5.50 },
            new Product { Location = 2, Producer = "Dumont", ProductName = "Myrtilles", Quantity = 12, Unit = "kg", PricePerUnit = 5.50 }
        };
        
        var frenchToEnglish = new Dictionary<string, string>()
        {
            {"Pommes", "Apples"},
            {"Poires", "Pears"},
            {"Pastèques", "Watermelons"},
            {"Melons", "Melons"},
            {"Noix", "Nuts"},
            {"Raisin", "Grapes"},
            {"Pruneaux", "Prunes"},
            {"Myrtilles", "Blueberries"}
        };
        
        var result = products.Select(p => new
        {
            ProducerAnon = $"{p.Producer.Substring(0, 3)}...{p.Producer[^1]}",
            ProductNameEn = frenchToEnglish.ContainsKey(p.ProductName) ? frenchToEnglish[p.ProductName] : p.ProductName,
            Revenue = p.Quantity * p.PricePerUnit
        }).ToList();

        foreach (var item in result)
        {
            Console.WriteLine($"Producer: {item.ProducerAnon}, Product: {item.ProductNameEn}, Revenue: {item.Revenue} $");
        }
        
        var csvLines = new List<string>();
        csvLines.Add("Seller,Product,CA");
        foreach (var item in result)
        {
            csvLines.Add($"{item.ProducerAnon},{item.ProductNameEn},{item.Revenue.ToString("F2").Replace(',', '.')}");
        }
        string csvContent = string.Join(Environment.NewLine, csvLines);
        string filePath = "export.csv";
        File.WriteAllText(filePath, csvContent);
        Console.WriteLine($"Fichier CSV exporté: {Path.GetFullPath(filePath)}");

        var result2 = products.Select(p => new
        {
            Anoyme = $"{p.Producer.Substring(0, 1)}...{p.Producer.Length}...{p.Producer[^1]}",

        }).ToList();
    }
    public class Product
    {
        public int Location { get; set; }            // Identifiant du lieu de production
        public string Producer { get; set; }         // Nom du producteur
        public string ProductName { get; set; }      // Nom du produit
        public double Quantity { get; set; }         // Quantité en stock
        public string Unit { get; set; }             // Unité de mesure (ex: kg, pièce)
        public double PricePerUnit { get; set; }     // Prix par unité
    }

}
