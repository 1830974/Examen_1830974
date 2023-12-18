using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StockLibrary
{
    public class StockService
    {
        //Chemin d'accès vers le dossier "Mes Documents" de l'utilisateur actuel
        private readonly string STOCK_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\stock-library-data\\liste-produits.json";

        public List<Produit> CorrigerPrixAvantTaxes(string CodeProduit)
        {
            FileStream? fs = null;
            List<Produit>? produits = new List<Produit>();

            // TODO: CorrigerPrixAvantTaxes: compléter,  corriger et commenter

            fs = new FileStream(STOCK_PATH, FileMode.Open);

                produits = JsonSerializer.Deserialize<List<Produit>>(fs);

                if (produits == null || produits.Count == 0)
                {
                    Console.WriteLine("Aucun produit trouvé dans le fichier JSON.");
                }
                else
                {
                    foreach (var produit in produits)
                    {
                    Console.WriteLine("{0} {1} {2}", produit.CodeProduit, produit.NomProduit, produit.PrixAvantTaxes);
                    }
                }

            return produits;
        }

        public void ListerLesProduits()
        {
            FileStream? fs = null;

            // TODO: ListerLesProduits: compléter,  corriger et commenter

            fs = new FileStream(STOCK_PATH, FileMode.Open);
               
                List<Produit> produits = JsonSerializer.Deserialize<List<Produit>>(fs);

                if (produits == null || produits.Count == 0)
                {
                    Console.WriteLine("Aucun produit trouvé dans le fichier JSON.");
                }
                else
                {
                    foreach (var produit in produits)
                    {
                        Console.WriteLine("{0} {1} {2}", produit.CodeProduit, produit.NomProduit, produit.PrixAvantTaxes);
                    }
                }
        }

        public void MettreaJourMesProduits(List<Produit> mesProduits)
        {
            // TODO: MettreaJourMesProduits: compléter,  corriger et commenter

            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Pour une sortie JSON formatée de manière lisible
            };

            using (FileStream stream = new FileStream(STOCK_PATH, FileMode.Create))
            {
                JsonSerializer.SerializeAsync(stream, mesProduits, options).Wait();
            }
        }


    }
}
