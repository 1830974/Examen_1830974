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

            fs = new FileStream(STOCK_PATH, FileMode.Open);

            //Ajout d'un try catch pour gèrer le exceptions de fichier
            try
            {
                produits = JsonSerializer.Deserialize<List<Produit>>(fs);

                if (produits == null || produits.Count == 0)
                {
                    Console.WriteLine("Aucun produit trouvé dans le fichier JSON.");
                }
                else
                {
                    foreach (var produit in produits)
                    {
                        if (produit.CodeProduit == CodeProduit)
                        {
                            //Total des taxes applicable sur un produit
                            double taxes = 1 + produit.TPS + produit.TVQ;

                            //Calcul du prix avant taxes dans le cas où les taxes ne sont pas de 0 (Éviter la division par 0)
                            if (taxes != 0)
                            {
                                produit.PrixAvantTaxes = produit.PrixApresTaxes / taxes;
                            }
                            else 
                            {
                                //Si les taxes étaient de 0, le prix après taxes est le même que celui avant taxes
                                produit.PrixAvantTaxes = produit.PrixApresTaxes;
                            }

                            //Affichage du produit et de son prix avant taxes
                            Console.WriteLine("{0} {1} {2}", produit.CodeProduit, produit.NomProduit, produit.PrixAvantTaxes);
                        }
                    }
                }
            }
            //Gestion des Exceptions
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur Innatendue : " + e);
            }


            return produits;
        }

        public void ListerLesProduits()
        {
            FileStream? fs = null;

            // TODO: ListerLesProduits: compléter,  corriger et commenter

            fs = new FileStream(STOCK_PATH, FileMode.Open);
               
            //Ajout d'un try catch pour gèrer le exceptions de fichier
            try
            {
                List<Produit> produits = JsonSerializer.Deserialize<List<Produit>>(fs);

                if (produits == null || produits.Count == 0)
                {
                    Console.WriteLine("Aucun produit trouvé dans le fichier JSON.");
                }
                else
                {
                    foreach (var produit in produits)
                    {
                        //Affichage de tout les données reliées à un produits
                        Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", 
                        produit.CodeProduit, produit.NomProduit, produit.Quantite,
                        produit.PrixAvantTaxes, produit.PrixApresTaxes, produit.TPS, produit.TVQ);
                    }
                }
            }
            //Gestion des Exceptions
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur Innatendue : " + e);
            }
               


        }

        public void MettreaJourMesProduits(List<Produit> mesProduits)
        {
            // TODO: MettreaJourMesProduits: compléter,  corriger et commenter

            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Pour une sortie JSON formatée de manière lisible
            };

            /*Si la liste de produits n'est pas vide, les produits voient leurs prix avant taxes être calculer dans le cas où ils sont 
            //identifiable par leur code de produit*/
            if (mesProduits != null && mesProduits.Count != 0)
            { 
                foreach (Produit produit in mesProduits)
                {
                    if (produit.CodeProduit != null && produit.CodeProduit != "")
                    {
                        //Correction du prix des produits dans la liste
                        CorrigerPrixAvantTaxes(produit.CodeProduit);
                    }
                    
                }
            }

            //Affichage des produits pour confirmer que nous allons sérialiser la bonne liste de produits
            if (mesProduits != null && mesProduits.Count != 0)
            {
                foreach (var produit in mesProduits)
                {
                    //Affichage de tout les données reliées à un produits
                    Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}",
                    produit.CodeProduit, produit.NomProduit, produit.Quantite,
                    produit.PrixAvantTaxes, produit.PrixApresTaxes, produit.TPS, produit.TVQ);
                }
            }


            //Si l'utilisateur appuie sur Y, la sérialisation commence, sinon elle ne s'effectue pas
            Console.WriteLine("Les produits suivant sont-il bien ceux à sérialiser? \n Y pour confirmer \t n'importe quelle autre touche pour annuler");
            ConsoleKey choix = Console.ReadKey().Key;

            if (choix == ConsoleKey.Y)
            {
                //Désignation du fichier sur lequel écrire ou qui est à créer
                using (FileStream stream = new FileStream(STOCK_PATH, FileMode.Create))
                {
                    try
                    {
                        //Attends que la sérialisation soit compléter avant de poursuivre
                        JsonSerializer.SerializeAsync(stream, mesProduits, options).Wait();
                    }
                    //Gestion des Exceptions
                    catch (UnauthorizedAccessException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erreur Innatendue : " + e);
                    }

                }
            }

        }

    }
}
