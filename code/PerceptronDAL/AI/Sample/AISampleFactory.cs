using Demo3tiers.DAL.AI.Sample.Type;
using Demo3tiers.DAL.TabularData;

/// <summary>
/// Démonstration du patron architectural 3 tiers et de plusieurs patrons de conception
/// 
/// (CC) BY-SA Stéphane Denis et Hugo St-Louis, CEGEP de Saint-Hyacinthe
/// </summary>
namespace Demo3tiers.DAL.AI.Sample
{
    /// <summary>
    /// Démonstration du patron Fabrique (Factory) pour créer des instances d'échantillons (sample) pour le perceptron
    /// https://fr.wikipedia.org/wiki/Fabrique_(patron_de_conception)
    /// </summary>
    public class AISampleFactory
    {
        /// <summary>
        /// Crée une objet du sous-type spécifié contenant les données d'entrée et résultat passés en argument
        /// </summary>
        /// <param name="perceptronSampleType">Nom de la classe du type de données</param>
        /// <param name="attributes">Données d'entrées du perceptron</param>
        /// <param name="result">Sortie associée aux données d'entrées</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static AISample Create(string perceptronSampleType, double[] attributes, int result)
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
        public static AISample[] CreateFromFile(string perceptronSampleType, string fileName)
        {
            List<AISample> samples = new List<AISample>();

            ITabularDataReader tabularDataReader = TabularDataReaderFactory.CreateDataReader(fileName);
            string[][] data = tabularDataReader.Read(fileName);

            foreach (string[] sample in data)
            {

                try
                {
                    List<double> attributes = new List<double>();
                    for (int i = 0; i < sample.Length - 1; i++) // Tout sauf la dernière colonne, car la dernière contient le champ réponse
                    {
                        attributes.Add(double.Parse(sample[i]));
                    }
                    int result = int.Parse(sample[sample.Length - 1]); // -1 car index débute à 0
                    samples.Add(Create(perceptronSampleType, attributes.ToArray(), result));
                }
                catch (FormatException ex)
                {
                    throw new InvalidOperationException(
                        "Le fichier \"" + fileName + "\" est mal formatté (données d'attributs)", ex);
                }
            }
            return samples.ToArray();
        }

    }
}
