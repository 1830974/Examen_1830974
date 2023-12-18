using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary
{
    public class Produit
    {
        public string? CodeProduit { get; set; }
        public string? NomProduit { get; set; }
        public int Quantite { get; set; }
        public double PrixAvantTaxes { get; set; }
        public double PrixApresTaxes { get; set; }
        public double TPS { get; set; }
        public double TVQ { get; set; }



        public Produit() { }

        public Produit(string _codeProduit, string _nomProduit, int _quantite, double _prixAvantTaxes, double _prixApresTaxes, double _TPS, double _TVQ)
        {
            CodeProduit = _codeProduit;
            NomProduit = _nomProduit;
            Quantite = _quantite;
            PrixAvantTaxes = _prixAvantTaxes;
            PrixApresTaxes = _prixApresTaxes;
            TPS = _TPS;
            TVQ = _TVQ;
        }
    }
}
