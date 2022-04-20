using Demo3tiers.DAL.AI.Sample;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3tiers.DAL.TabularData
{
    internal class DatReader : ITabularDataReader
    {
        public string[][] Read(string fileName)
        {

            List<string[]> table = new List<string[]>();

            if (!fileName.EndsWith(".dat"))
            {
                throw new InvalidOperationException(
                    "Le nom de fichier doit finir par .dat");
            }

            if (!File.Exists(fileName))
            {
                throw new InvalidOperationException(
                    "Le fichier \"" + fileName + "\" est introuvable.");
            }

            try
            {
                int lines;
                int columns;
                StreamReader sr = new StreamReader(fileName);
                int currentLine = 0;
                try
                {
                    lines = int.Parse(sr.ReadLine());
                    columns = int.Parse(sr.ReadLine());
                    currentLine += 2;
                }
                catch (Exception ex) when (ex is NullReferenceException || ex is FormatException)
                {
                    throw new InvalidOperationException(
                        "Le fichier \"" + fileName + "\" est mal formatté (entête)", ex);
                }

                int linesRead = 0; // pour valider le nombre d'entrées en rapport avec celui déclaré en entête
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    currentLine++;
                    if (line == null)
                    {
                        throw new InvalidOperationException(
                            "Le fichier \"" + fileName + "\" contient une ligne vide à un endroit innattendu. (ligne "+ currentLine+")");
                    }

                    string[] dataLine = line.Split('\t');
                    if (dataLine.Length != columns)
                    {
                        throw new InvalidOperationException(
                            "Le fichier \"" + fileName + "\" contient une ligne avec un nombre de colonnes innattendu (" + dataLine.Length + "/" + columns + ") à la ligne " + currentLine + ".");
                    }

                    List<string> fields = new List<string>();
                    for (int i = 0; i < dataLine.Length; i++)
                    {
                        fields.Add(dataLine[i]);
                    }
                    table.Add(fields.ToArray());

                    linesRead++;
                }
                if (linesRead != lines)
                {
                    throw new InvalidOperationException(
                        "Le fichier \"" + fileName + "\" contient un nombre d'entrées innattendu (" + linesRead + "/" + lines + ") à la ligne " + currentLine + ".");
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
