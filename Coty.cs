using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Coty
{
    public class Coty
    {
        private string sql = "";

        public static void Main(string[] args)
        {
            var path = @"D:\Documents\Visual Studio 2017\Projects\Coty\Coty\coty.csv";
            if (!File.Exists(path))
                throw new Exception();

            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                var lineSplit = SplitLine(line);
                famille famille = getFamille(lineSplit);

                switch (famille)
                {
                    case famille.produit:
                        break;
                    case famille.marque:
                        break;
                    case famille.error:
                        break;
                };
            }
        }

//        private static void traitementProduit(IEnumerable<string> lineSplit)
//        {
//            var patternRef = @"[0-9]{3}";
            
//            Regex ref = new Regex(patternRef);

////            test a faire sur les champs :
////- ref fournisseur : string de 40 char max contenant uniquement des chiffres(table sql -> varchar 40, fichier excel que des chiffres)
////-pr_desi : concatenation des colonne C et D max 100 char alphanum(di déjà la qtt : rajoute quand même)
////-prix achat: float avec 2 chiffre après la virgule et 8 avant
////- prix vente: float avec 2 chiffre après la virgule et 8 avant
////- code barre: big int(14 digits)
////- marque : varchar 30
//        }

        private static famille getFamille(IEnumerable<string> lineSplit)
        {
            return famille.error;
        }

        private static IEnumerable<string> SplitLine(string line)
        {
            return line.Split(';').ToArray();
        }

        private enum famille
        {
            produit = 0,
            marque = 1,
            error = 2
        }

    }
}
