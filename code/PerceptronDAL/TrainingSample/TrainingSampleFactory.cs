using Perceptron.DAL.TrainingSample.Type;

/// <summary>
/// Démonstration du patron architectural 3 tiers et de plusieurs patrons de conception
/// 
/// (CC) BY-SA Stéphane Denis et Hugo St-Louis, CEGEP de Saint-Hyacinthe
/// </summary>
namespace Perceptron.DAL.TrainingSample
{
    /// <summary>
    /// Démonstration du patron Fabrique (Factory) pour créer des instances d'échantillons (sample) pour le perceptron
    /// https://fr.wikipedia.org/wiki/Fabrique_(patron_de_conception)
    /// </summary>
    public class TrainingSampleFactory
    {
        /// <summary>
        /// Crée une objet du sous-type spécifié contenant les données d'entrée et résultat passés en argument
        /// </summary>
        /// <param name="perceptronSampleType">Nom de la classe du type de données</param>
        /// <param name="attributes">Données d'entrées du perceptron</param>
        /// <param name="result">Sortie associée aux données d'entrées</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static TrainingSample Create(string perceptronSampleType, float[] attributes, int result)
        {
            if (perceptronSampleType.Equals(typeof(BreastCancerSample).Name))
            {
                return new BreastCancerSample(attributes, result);
            }
            if (perceptronSampleType.Equals(typeof(DiabeteSample).Name))
            {
                return new DiabeteSample(attributes, result);
            }
            if (perceptronSampleType.Equals(typeof(SonarSample).Name))
            {
                return new SonarSample(attributes, result);
            }
            if (perceptronSampleType.Equals(typeof(USVoteSample).Name))
            {
                return new USVoteSample(attributes, result);
            }
            throw new InvalidOperationException(perceptronSampleType + " is not defined in the factory");
        }

        /// <summary>
        /// Crée un tableau d'objets du type fourni à partir des données d'un fichier .dat
        /// 
        /// Un fichier .dat commence par un nombre sur la première ligne pour dire combien d'entrées sont dans le fichier
        /// La seconde ligne contient le nombre de champs (colonnes)
        /// Les autres lignes sont les données sous forme tabulaires : 
        ///     le caractère TAB délimite les champs et 
        ///     le saut de ligne délimite les entrées.
        /// 
        /// </summary>
        /// <param name="perceptronSampleType">Nom de la classe du type de données</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static TrainingSample[] CreateFromDatFile(string perceptronSampleType, string fileName)
        {
            List<TrainingSample> samples = new List<TrainingSample>();

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
                try
                {
                    lines = int.Parse(sr.ReadLine());
                    columns = int.Parse(sr.ReadLine());
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
                    if(line == null)
                    {
                        throw new InvalidOperationException(
                            "Le fichier \"" + fileName + "\" contient une ligne vide à un endroit innattendu.");
                    }

                    string[] dataLine = line.Split('\t');
                    if (dataLine.Length != columns)
                    {
                        throw new InvalidOperationException(
                            "Le fichier \"" + fileName + "\" contient une ligne avec un nombre de colonnes innattendu ("+ dataLine.Length+"/"+columns+").");
                    }
                    try {
                        List<float> attributes = new List<float>();
                        for(int i = 0; i < dataLine.Length-1; i++) // Tout sauf la dernière colonne, car la dernière contient le champ réponse
                        {
                            attributes.Add( float.Parse(dataLine[i]));
                        }
                        int result = int.Parse(dataLine[dataLine.Length - 1]); // -1 car index débute à 0
                        samples.Add(Create(perceptronSampleType, attributes.ToArray(), result));
                    }
                    catch (FormatException ex)
                    {
                        throw new InvalidOperationException(
                            "Le fichier \"" + fileName + "\" est mal formatté (données d'attributs)", ex);
                    }
                    linesRead++;
                }
                if(linesRead != lines)
                {
                    throw new InvalidOperationException(
                        "Le fichier \"" + fileName + "\" contient un nombre d'entrées innattendu ("+linesRead+"/"+lines+").");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erreur lors de la lecture du fichier \"" + fileName + "\". ", ex);
            }

            return samples.ToArray();
        }

    }
}
