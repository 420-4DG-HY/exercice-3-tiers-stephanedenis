using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3tiers.DAL.TabularData
{
    internal class CsvReader : ITabularDataReader
    {
        public string[][] Read(string fileName)
        {
            char delimiter = '\t';
            // TODO supporter éventuellement plusieurs délimiteurs, dont ';' ou ','
            // TODO supporter la ligne d'en-tête en option, avec le nom des champs

            List<string[]> table = new List<string[]>();

            if (!fileName.EndsWith(".csv"))
            {
                throw new InvalidOperationException(
                    "Le nom de fichier doit finir par .csv");
            }

            if (!File.Exists(fileName))
            {
                throw new InvalidOperationException(
                    "Le fichier \"" + fileName + "\" est introuvable.");
            }

            try
            {
                StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    if (line == null)
                    {
                        throw new InvalidOperationException(
                            "Le fichier \"" + fileName + "\" contient une ligne vide à un endroit innattendu.");
                    }

                    string[] dataLine = line.Split(delimiter);

                    List<string> fields = new List<string>();
                    for (int i = 0; i < dataLine.Length; i++)
                    {
                        fields.Add(dataLine[i]);
                    }
                    table.Add(fields.ToArray());

                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erreur lors de la lecture du fichier \"" + fileName + "\". ", ex);
            }

            return table.ToArray();
        }
    }
}
