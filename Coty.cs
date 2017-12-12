using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Coty
{
    public class Coty
    {
        private static string sql = "";
        private static string currentMarque = "";
        private static int ID = 1000000;

        public static void Main(string[] args)
        {
            var path = @"D:\Documents\Visual Studio 2017\Projects\Coty\Coty\";
            var csv = path + "coty.csv";
            if (!File.Exists(csv))
                throw new Exception();

            var lines = File.ReadAllLines(csv);
            currentMarque = "HUGO BOSS";
            sql += "INSERT INTO `produit` (`pr_cd_pr`, `pr_desi`, `pr_stre`, `pr_douane`, `pr_prac`, `pr_deg`, `pr_pdn`, `pr_pdb`, `pr_four`, `pr_refour`, `pr_codebarre`, `pr_marque`, `pr_prix`, `pr_pack`) VALUES \n";
            foreach (string line in lines)
            {
                var lineSplit = SplitLine(line);
                famille famille = getFamille(lineSplit);

                switch (famille)
                {
                    case famille.produit:
                        var lineSql = traitementProduit(lineSplit);
                        writeSql(lineSql);
                        break;
                    case famille.marque:
                        currentMarque = lineSplit.ElementAt(2);
                        break;
                    case famille.error:
                        break;
                };
            }
            Console.Write(sql);

            var res = path + "res.sql";
            if (File.Exists(res))
            {
                File.Delete(res);
            }
            using (StreamWriter sw = File.CreateText(res))
            {
                sw.WriteLine(sql);
            }
            }

        private static IEnumerable<string> traitementProduit(IEnumerable<string> lineSplit)
        {
            string patternRef = @"\d{1,40}";
            Regex rgxRef = new Regex(patternRef, RegexOptions.IgnoreCase);
            if (!rgxRef.IsMatch(lineSplit.ElementAt(0)))
                throw new Exception();

            string patternPrDesi = @".{1,100}";
           Regex rgxPrDesi = new Regex(patternPrDesi, RegexOptions.IgnoreCase);
            var prDesi = lineSplit.ElementAt(2) + lineSplit.ElementAt(3);
            if (!rgxPrDesi.IsMatch(prDesi))
                throw new Exception();

            string patternPrix = @"\d{1,8}[,]\d{2}";
            Regex rgxPrix = new Regex(patternPrix, RegexOptions.IgnoreCase);

            if (!rgxPrix.IsMatch(lineSplit.ElementAt(5)))
                throw new Exception();
            if (!rgxPrix.IsMatch(lineSplit.ElementAt(6)))
                throw new Exception();

            string patternBarre = @"\d{10,14}";
            Regex rgxBarre = new Regex(patternBarre, RegexOptions.IgnoreCase);
            if (!(rgxBarre.IsMatch(lineSplit.ElementAt(7)) || lineSplit.ElementAt(7) == null))
                throw new Exception();

            string patternMarque = @".{1,30}";
            Regex rgxMarque = new Regex(patternMarque, RegexOptions.IgnoreCase);
            if (!rgxMarque.IsMatch(currentMarque))
                throw new Exception();

            var result = new string[] {
                ID.ToString(),
                (currentMarque + prDesi + "Ml").ToString(),
                "0",
                "0",
                lineSplit.ElementAt(5),
                "0",
                "0",
                "0",
                "0",
                lineSplit.ElementAt(0),
                lineSplit.ElementAt(7),
                (currentMarque + " ").ToString(),
                lineSplit.ElementAt(6),
                lineSplit.ElementAt(4),
            };
            ID++;
            return result.ToArray();
        }

        private static void writeSql(IEnumerable<string> lineSql)
        {
            sql += "('" +
                lineSql.ElementAt(0) +
                "`, `" +
                lineSql.ElementAt(1) +
                "`, `" +
                lineSql.ElementAt(2) +
                "`, `" +
                lineSql.ElementAt(3) +
                "`, `" +
                lineSql.ElementAt(4) +
                "`, `" +
                lineSql.ElementAt(5) +
                "`, `" +
                lineSql.ElementAt(6) +
                "`, `" +
                lineSql.ElementAt(7) +
                "`, `" +
                lineSql.ElementAt(8) +
                "`, `" +
                lineSql.ElementAt(9) +
                "`, `" +
                lineSql.ElementAt(10) +
                "`, `" +
                lineSql.ElementAt(11) +
                "`, `" +
                lineSql.ElementAt(12) +
                "`, `" +
                lineSql.ElementAt(13) +
                ")";
            sql += "\n";
        }

        private static famille getFamille(IEnumerable<string> lineSplit)
        {
            string patternRef = @"\d{1,40}";
            Regex rgxRef = new Regex(patternRef, RegexOptions.IgnoreCase);
            if (rgxRef.IsMatch(lineSplit.ElementAt(0)))
                return famille.produit;

            if (lineSplit.ElementAt(0).Equals("") && !lineSplit.ElementAt(2).Equals(""))
                return famille.marque;

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
